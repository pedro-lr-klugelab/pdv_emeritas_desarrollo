# Script para ver todas las sucursales

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Código de Sucursal Actual" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Mostrar todas las sucursales
Write-Host "Todas las sucursales disponibles:" -ForegroundColor Yellow
Write-Host ""

$sucursales = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
SELECT 
    sucursal_id as ID,
    etiqueta as CODIGO,
    nombre as NOMBRE,
    ciudad as CIUDAD,
    CASE WHEN es_farmacontrol = 1 THEN 'ACTIVA' ELSE 'INACTIVA' END as ESTADO
FROM 
    sucursales 
ORDER BY 
    sucursal_id;
" 2>&1

Write-Host $sucursales
Write-Host ""

Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Para saber tu sucursal actual, verifica en la aplicación" -ForegroundColor Yellow
Write-Host "o pregunta a tu administrador." -ForegroundColor Yellow
Write-Host ""
Write-Host "El ID de sucursal es el número que necesitas usar" -ForegroundColor White
Write-Host "en los otros scripts de búsqueda." -ForegroundColor White
Write-Host ""
