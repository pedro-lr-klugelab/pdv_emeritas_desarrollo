@echo off
echo ========================================
echo   Estructura de Tabla Empleados
echo ========================================
echo.

echo Mostrando estructura de la tabla empleados:
echo.
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "DESCRIBE empleados;"

echo.
echo ========================================
echo   Primeros 5 Empleados
echo ========================================
echo.
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT * FROM empleados LIMIT 5;"

echo.
pause
