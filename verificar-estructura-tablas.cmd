@echo off
echo ========================================
echo   Verificar Estructura de Tablas
echo ========================================
echo.

echo 1. Estructura de tabla SUCURSALES:
echo.
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "DESCRIBE sucursales;"
echo.

echo 2. Estructura de tabla ARTICULOS:
echo.
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "DESCRIBE articulos;"
echo.

echo 3. Estructura de tabla EXISTENCIAS:
echo.
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local -e "DESCRIBE existencias;"
echo.

echo 4. Primeros 3 registros de SUCURSALES:
echo.
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT * FROM sucursales LIMIT 3;"
echo.

pause
