# Script para limpiar archivos SQL de comandos de replicación
# Esto eliminará las líneas problemáticas que causan errores de importación

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Limpiando Archivos SQL" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$archivos = @("globalchuburna01.sql", "localchuburna01.sql")

foreach ($archivo in $archivos) {
    if (-not (Test-Path $archivo)) {
        Write-Host "✗ No se encuentra: $archivo" -ForegroundColor Red
        continue
    }
    
    Write-Host "Procesando $archivo..." -ForegroundColor Yellow
    
    # Crear backup
    $backup = "$archivo.backup"
    if (-not (Test-Path $backup)) {
        Copy-Item $archivo $backup
        Write-Host "  ✓ Backup creado: $backup" -ForegroundColor Green
    }
    
    # Leer archivo
    Write-Host "  Leyendo archivo (esto puede tomar un minuto)..." -ForegroundColor Gray
    $content = Get-Content $archivo -Raw
    
    # Eliminar comandos de replicación y otros problemas comunes
    Write-Host "  Eliminando comandos problemáticos..." -ForegroundColor Gray
    
    # Comandos de replicación
    $content = $content -replace "CHANGE MASTER TO.*?;", ""
    $content = $content -replace "STOP SLAVE;", ""
    $content = $content -replace "START SLAVE;", ""
    $content = $content -replace "RESET SLAVE;", ""
    
    # Comandos GTID
    $content = $content -replace "SET \@\@SESSION\.SQL_LOG_BIN.*?;", ""
    $content = $content -replace "SET \@\@GLOBAL\.GTID_PURGED.*?;", ""
    
    # Guardar archivo limpio
    Write-Host "  Guardando archivo limpio..." -ForegroundColor Gray
    $content | Set-Content -Path $archivo -Encoding UTF8
    
    Write-Host "✓ $archivo procesado correctamente" -ForegroundColor Green
    Write-Host ""
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Limpieza Completada" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Los archivos SQL han sido limpiados y están listos para importar." -ForegroundColor Green
Write-Host "Se crearon archivos .backup por seguridad." -ForegroundColor Yellow
Write-Host ""
Write-Host "Ahora puedes ejecutar: .\import-now.ps1" -ForegroundColor Cyan
Write-Host ""
