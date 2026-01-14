@echo off
echo ========================================
echo   Buscar Productos AMECOP
echo   Alta Disponibilidad
echo ========================================
echo.
echo NOTA: El sistema maneja existencias globales, no por sucursal.
echo.

echo Buscando productos con codigo AMECOP y alta disponibilidad...
echo (Productos con existencia mayor a 10 unidades)
echo.

docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT a.amecop_original as AMECOP, a.nombre as PRODUCTO, (SELECT SUM(e.existencia) FROM farmacontrol_local.existencias e WHERE e.articulo_id = a.articulo_id) as EXISTENCIA, a.precio_publico as PRECIO FROM articulos a WHERE a.amecop_original IS NOT NULL AND (SELECT SUM(e.existencia) FROM farmacontrol_local.existencias e WHERE e.articulo_id = a.articulo_id) >= 10 ORDER BY EXISTENCIA DESC LIMIT 50;"

echo.
echo ========================================
echo   Top 50 productos con mayor existencia
echo ========================================
echo.
pause
