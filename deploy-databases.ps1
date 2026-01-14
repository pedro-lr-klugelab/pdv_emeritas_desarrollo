# Script de Despliegue de Bases de Datos Farmacontrol
# Este script configura y despliega las bases de datos MySQL en Docker

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Farmacontrol PDV - Despliegue MySQL  " -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar que Docker está corriendo
Write-Host "Verificando Docker Desktop..." -ForegroundColor Yellow
$dockerRunning = Get-Process "Docker Desktop" -ErrorAction SilentlyContinue
if (-not $dockerRunning) {
    Write-Host "ERROR: Docker Desktop no está corriendo." -ForegroundColor Red
    Write-Host "Por favor, inicia Docker Desktop e intenta nuevamente." -ForegroundColor Red
    exit 1
}
Write-Host "✓ Docker Desktop está corriendo" -ForegroundColor Green
Write-Host ""

# Verificar archivos SQL necesarios
Write-Host "Verificando archivos SQL necesarios..." -ForegroundColor Yellow
$archivosRequeridos = @(
    "00-init-databases.sql",
    "globalchuburna01.sql",
    "localchuburna01.sql",
    "docker-compose.yml"
)

$archivosNoEncontrados = @()
foreach ($archivo in $archivosRequeridos) {
    if (-not (Test-Path $archivo)) {
        $archivosNoEncontrados += $archivo
        Write-Host "✗ No encontrado: $archivo" -ForegroundColor Red
    } else {
        $fileInfo = Get-Item $archivo
        $sizeMB = [math]::Round($fileInfo.Length/1MB,2)
        Write-Host "✓ Encontrado: $archivo ($sizeMB MB)" -ForegroundColor Green
    }
}

if ($archivosNoEncontrados.Count -gt 0) {
    Write-Host ""
    Write-Host "ERROR: Faltan archivos necesarios." -ForegroundColor Red
    Write-Host "Asegúrate de ejecutar este script desde el directorio raíz del proyecto." -ForegroundColor Red
    exit 1
}
Write-Host ""

# Advertencia sobre datos existentes
Write-Host "ADVERTENCIA: Este proceso eliminará cualquier contenedor y datos existentes." -ForegroundColor Yellow
Write-Host "Si tienes datos importantes, haz un backup antes de continuar." -ForegroundColor Yellow
Write-Host ""
Write-Host "NOTA: La importación de archivos grandes puede tomar 10-20 minutos." -ForegroundColor Yellow
Write-Host ""

$continuar = Read-Host "¿Deseas continuar? (S/N)"
if ($continuar -ne "S" -and $continuar -ne "s") {
    Write-Host "Operación cancelada por el usuario." -ForegroundColor Yellow
    exit 0
}
Write-Host ""

# Detener y eliminar contenedores existentes
Write-Host "Deteniendo contenedores existentes..." -ForegroundColor Yellow
docker-compose down -v 2>&1 | Out-Null
Write-Host "✓ Contenedores detenidos y volúmenes eliminados" -ForegroundColor Green
Write-Host ""

# Iniciar el contenedor
Write-Host "Iniciando contenedor MySQL..." -ForegroundColor Yellow
docker-compose up -d

if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "ERROR: Falló al iniciar el contenedor." -ForegroundColor Red
    Write-Host "Revisa los logs con: docker-compose logs mysql" -ForegroundColor Red
    exit 1
}

Write-Host "✓ Contenedor iniciado" -ForegroundColor Green
Write-Host ""

# Esperar a que MySQL esté listo
Write-Host "Esperando a que MySQL esté listo..." -ForegroundColor Yellow
$intentos = 0
$maxIntentos = 60

do {
    $intentos++
    Start-Sleep -Seconds 2
    
    $mysqlListo = docker exec farmacontrol_mysql mysqladmin ping -h localhost -uroot -prootpassword 2>&1 | Select-String "mysqld is alive"
    
    if ($mysqlListo) {
        break
    }
    
    if ($intentos -ge $maxIntentos) {
        Write-Host ""
        Write-Host "ERROR: MySQL no respondió después de $($maxIntentos * 2) segundos." -ForegroundColor Red
        Write-Host "Revisa los logs con: docker-compose logs mysql" -ForegroundColor Red
        exit 1
    }
    
    Write-Host "." -NoNewline -ForegroundColor Yellow
} while ($true)

Write-Host ""
Write-Host "✓ MySQL está listo" -ForegroundColor Green
Write-Host ""

# Verificar que las bases de datos se hayan creado
Write-Host "Verificando creación de bases de datos..." -ForegroundColor Yellow
$databases = docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SHOW DATABASES;" 2>&1

if ($databases -match "farmacontrol_global" -and $databases -match "farmacontrol_local") {
    Write-Host "✓ Bases de datos creadas correctamente" -ForegroundColor Green
} else {
    Write-Host "✗ ERROR: No se crearon las bases de datos." -ForegroundColor Red
    Write-Host "Bases de datos encontradas:" -ForegroundColor Yellow
    Write-Host $databases
    exit 1
}
Write-Host ""

# Importar farmacontrol_global
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Importando farmacontrol_global..." -ForegroundColor Cyan
Write-Host "Esto puede tomar 10-15 minutos..." -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$startTime = Get-Date
docker exec -i farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global < globalchuburna01.sql

if ($LASTEXITCODE -eq 0) {
    $duration = [math]::Round(((Get-Date) - $startTime).TotalMinutes, 2)
    Write-Host "✓ farmacontrol_global importada correctamente ($duration minutos)" -ForegroundColor Green
} else {
    Write-Host "✗ ERROR al importar farmacontrol_global" -ForegroundColor Red
    Write-Host "Revisa los logs con: docker-compose logs mysql" -ForegroundColor Red
    exit 1
}
Write-Host ""

# Verificar tablas en farmacontrol_global
Write-Host "Verificando tablas en farmacontrol_global..." -ForegroundColor Yellow
$tablasGlobal = docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SELECT COUNT(*) as total FROM information_schema.tables WHERE table_schema = 'farmacontrol_global';" 2>&1 | Select-String -Pattern '\d+' | Select-Object -Last 1
Write-Host "✓ Tablas en farmacontrol_global: $($tablasGlobal)" -ForegroundColor Green
Write-Host ""

# Importar farmacontrol_local
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Importando farmacontrol_local..." -ForegroundColor Cyan
Write-Host "Esto puede tomar 10-15 minutos..." -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$startTime = Get-Date
docker exec -i farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local < localchuburna01.sql

if ($LASTEXITCODE -eq 0) {
    $duration = [math]::Round(((Get-Date) - $startTime).TotalMinutes, 2)
    Write-Host "✓ farmacontrol_local importada correctamente ($duration minutos)" -ForegroundColor Green
} else {
    Write-Host "✗ ERROR al importar farmacontrol_local" -ForegroundColor Red
    Write-Host "Revisa los logs con: docker-compose logs mysql" -ForegroundColor Red
    exit 1
}
Write-Host ""

# Verificar tablas en farmacontrol_local
Write-Host "Verificando tablas en farmacontrol_local..." -ForegroundColor Yellow
$tablasLocal = docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SELECT COUNT(*) as total FROM information_schema.tables WHERE table_schema = 'farmacontrol_local';" 2>&1 | Select-String -Pattern '\d+' | Select-Object -Last 1
Write-Host "✓ Tablas en farmacontrol_local: $($tablasLocal)" -ForegroundColor Green
Write-Host ""

# Obtener conteo detallado de tablas
Write-Host "Obteniendo lista de tablas..." -ForegroundColor Yellow
$tablasGlobalDetalle = docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "USE farmacontrol_global; SHOW TABLES;" 2>&1
$numTablasGlobal = ($tablasGlobalDetalle | Measure-Object -Line).Lines - 1

$tablasLocalDetalle = docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "USE farmacontrol_local; SHOW TABLES;" 2>&1
$numTablasLocal = ($tablasLocalDetalle | Measure-Object -Line).Lines - 1

Write-Host "✓ farmacontrol_global: $numTablasGlobal tablas" -ForegroundColor Green
Write-Host "✓ farmacontrol_local: $numTablasLocal tablas" -ForegroundColor Green
Write-Host ""

# Resumen final
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "         DESPLIEGUE COMPLETADO          " -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Información de conexión:" -ForegroundColor White
Write-Host "  Host:     localhost" -ForegroundColor White
Write-Host "  Puerto:   3306" -ForegroundColor White
Write-Host "  Usuario:  farmacontrol" -ForegroundColor White
Write-Host "  Password: farmacontrol123" -ForegroundColor White
Write-Host ""
Write-Host "Bases de datos disponibles:" -ForegroundColor White
Write-Host "  - farmacontrol_global ($numTablasGlobal tablas)" -ForegroundColor White
Write-Host "  - farmacontrol_local ($numTablasLocal tablas)" -ForegroundColor White
Write-Host ""
Write-Host "Comandos útiles:" -ForegroundColor White
Write-Host "  Ver logs:       docker-compose logs -f mysql" -ForegroundColor Gray
Write-Host "  Detener:        docker-compose down" -ForegroundColor Gray
Write-Host "  Reiniciar:      docker-compose restart" -ForegroundColor Gray
Write-Host "  Conectar MySQL: docker exec -it farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123" -ForegroundColor Gray
Write-Host ""
Write-Host "La aplicación ahora puede conectarse a las bases de datos." -ForegroundColor Green
Write-Host ""
