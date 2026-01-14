@echo off
echo ========================================
echo   Codigo de Sucursal Actual
echo ========================================
echo.

REM Mostrar todas las sucursales
echo Todas las sucursales disponibles:
echo.
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT sucursal_id as ID, etiqueta as CODIGO, nombre as NOMBRE, ciudad as CIUDAD FROM sucursales ORDER BY sucursal_id;"

echo.
echo ========================================
echo.
echo Para saber tu sucursal actual, verifica en la aplicacion
echo o pregunta a tu administrador.
echo.
pause
