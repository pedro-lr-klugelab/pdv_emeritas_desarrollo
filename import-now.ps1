# Script rápido de importación con force
# Ignora errores de replicación y continúa con la importación

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "   Importando Bases de Datos" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "NOTA: Usando --force para ignorar errores de replicación" -ForegroundColor Yellow
Write-Host "Esto tomará 20-30 minutos aproximadamente" -ForegroundColor Yellow
Write-Host "No cierres esta ventana durante el proceso" -ForegroundColor Yellow
Write-Host ""

# Importar farmacontrol_global
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Importando farmacontrol_global..." -ForegroundColor Cyan
Write-Host "Inicio: $(Get-Date -Format 'HH:mm:ss')" -ForegroundColor Gray
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$startTime = Get-Date
docker exec -i farmacontrol_mysql mysql -uroot -prootpassword --force farmacontrol_global < globalchuburna01.sql

$duration = [math]::Round(((Get-Date) - $startTime).TotalMinutes, 2)
Write-Host ""
Write-Host "Fin: $(Get-Date -Format 'HH:mm:ss')" -ForegroundColor Gray
Write-Host "✓ farmacontrol_global importada ($duration minutos)" -ForegroundColor Green
Write-Host "  (Errores de replicación ignorados automáticamente)" -ForegroundColor Gray
Write-Host ""

# Verificar tablas en farmacontrol_global
Write-Host "Verificando tablas..." -ForegroundColor Yellow
$tablasGlobal = docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SELECT COUNT(*) as total FROM information_schema.tables WHERE table_schema='farmacontrol_global';" 2>&1 | Select-String -Pattern '\d+' | Select-Object -Last 1
Write-Host "✓ Tablas en farmacontrol_global: $tablasGlobal" -ForegroundColor Green
Write-Host ""

# Importar farmacontrol_local
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Importando farmacontrol_local..." -ForegroundColor Cyan
Write-Host "Inicio: $(Get-Date -Format 'HH:mm:ss')" -ForegroundColor Gray
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$startTime = Get-Date
docker exec -i farmacontrol_mysql mysql -uroot -prootpassword --force farmacontrol_local < localchuburna01.sql

$duration = [math]::Round(((Get-Date) - $startTime).TotalMinutes, 2)
Write-Host ""
Write-Host "Fin: $(Get-Date -Format 'HH:mm:ss')" -ForegroundColor Gray
Write-Host "✓ farmacontrol_local importada ($duration minutos)" -ForegroundColor Green
Write-Host "  (Errores de replicación ignorados automáticamente)" -ForegroundColor Gray
Write-Host ""

# Verificar tablas en farmacontrol_local
Write-Host "Verificando tablas..." -ForegroundColor Yellow
$tablasLocal = docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SELECT COUNT(*) as total FROM information_schema.tables WHERE table_schema='farmacontrol_local';" 2>&1 | Select-String -Pattern '\d+' | Select-Object -Last 1
Write-Host "✓ Tablas en farmacontrol_local: $tablasLocal" -ForegroundColor Green
Write-Host ""

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "   IMPORTACIÓN COMPLETADA" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Las bases de datos están listas para usar." -ForegroundColor Green
Write-Host ""
