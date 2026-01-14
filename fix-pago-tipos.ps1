# Solucionar error de pago_tipos

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Solucionar Error pago_tipos" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "Creando vista pago_tipos en farmacontrol_local..." -ForegroundColor Yellow
Write-Host ""

$resultado = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local -e "CREATE OR REPLACE VIEW pago_tipos AS SELECT * FROM farmacontrol_global.pago_tipos;" 2>&1

if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "ERROR: No se pudo crear la vista" -ForegroundColor Red
    Write-Host $resultado
    exit 1
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Vista creada correctamente" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "Verificando la vista..." -ForegroundColor Yellow
$verificacion = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local -e "SELECT * FROM pago_tipos LIMIT 5;" 2>&1

Write-Host $verificacion
Write-Host ""

Write-Host "SOLUCIÓN APLICADA:" -ForegroundColor Green
Write-Host "La vista 'pago_tipos' en farmacontrol_local ahora apunta a farmacontrol_global.pago_tipos" -ForegroundColor White
Write-Host ""
Write-Host "El programa debería funcionar correctamente ahora." -ForegroundColor Green
Write-Host ""
