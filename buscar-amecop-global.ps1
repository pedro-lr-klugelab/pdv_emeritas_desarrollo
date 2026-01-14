# Script para buscar productos AMECOP con alta disponibilidad

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Buscar Productos AMECOP" -ForegroundColor Cyan
Write-Host "  Alta Disponibilidad" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "NOTA: El sistema maneja existencias globales, no por sucursal." -ForegroundColor Yellow
Write-Host ""

Write-Host "Buscando productos con código AMECOP y alta disponibilidad..." -ForegroundColor Yellow
Write-Host "(Productos con existencia mayor a 10 unidades)" -ForegroundColor Gray
Write-Host ""

# Buscar productos con alta disponibilidad
$resultado = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
SELECT 
    a.amecop_original as AMECOP,
    a.nombre as PRODUCTO,
    (SELECT SUM(e.existencia) 
     FROM farmacontrol_local.existencias e 
     WHERE e.articulo_id = a.articulo_id) as EXISTENCIA,
    a.precio_publico as PRECIO
FROM 
    articulos a
WHERE 
    a.amecop_original IS NOT NULL 
    AND (SELECT SUM(e.existencia) 
         FROM farmacontrol_local.existencias e 
         WHERE e.articulo_id = a.articulo_id) >= 10
ORDER BY 
    EXISTENCIA DESC
LIMIT 50;
" 2>&1

Write-Host $resultado
Write-Host ""

# Mostrar estadísticas
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Estadísticas Generales" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$stats = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local -e "
SELECT 
    COUNT(DISTINCT a.articulo_id) as TOTAL_PRODUCTOS_CON_AMECOP,
    SUM(e.existencia) as TOTAL_UNIDADES,
    AVG(e.existencia) as PROMEDIO_EXISTENCIA
FROM 
    existencias e
INNER JOIN
    farmacontrol_global.articulos a ON e.articulo_id = a.articulo_id
WHERE 
    a.amecop_original IS NOT NULL 
    AND e.existencia >= 10;
" 2>&1

Write-Host $stats
Write-Host ""

# Opción para exportar a CSV
$exportar = Read-Host "¿Deseas exportar los resultados a CSV? (S/N)"

if ($exportar -eq "S" -or $exportar -eq "s") {
    $fecha = Get-Date -Format "yyyyMMdd_HHmmss"
    $archivo = "productos_amecop_${fecha}.csv"
    
    docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
    SELECT 
        a.amecop_original as AMECOP,
        a.nombre as PRODUCTO,
        (SELECT SUM(e.existencia) 
         FROM farmacontrol_local.existencias e 
         WHERE e.articulo_id = a.articulo_id) as EXISTENCIA,
        a.precio_publico as PRECIO
    FROM 
        articulos a
    WHERE 
        a.amecop_original IS NOT NULL 
        AND (SELECT SUM(e.existencia) 
             FROM farmacontrol_local.existencias e 
             WHERE e.articulo_id = a.articulo_id) >= 10
    ORDER BY 
        EXISTENCIA DESC;
    " | Out-File -FilePath $archivo -Encoding UTF8
    
    Write-Host "✓ Archivo exportado: $archivo" -ForegroundColor Green
}

Write-Host ""
Write-Host "Búsqueda completada" -ForegroundColor Green
Write-Host ""
