@echo off
echo ========================================
echo   Solucionar Error pago_tipos
echo ========================================
echo.

echo Creando vista pago_tipos en farmacontrol_local...
echo.

docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local -e "CREATE OR REPLACE VIEW pago_tipos AS SELECT * FROM farmacontrol_global.pago_tipos;"

if errorlevel 1 (
    echo.
    echo ERROR: No se pudo crear la vista
    pause
    exit /b 1
)

echo.
echo ========================================
echo   Vista creada correctamente
echo ========================================
echo.

echo Verificando la vista...
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local -e "SELECT * FROM pago_tipos LIMIT 5;"

echo.
echo SOLUCION APLICADA:
echo La vista 'pago_tipos' en farmacontrol_local ahora apunta a farmacontrol_global.pago_tipos
echo.
echo El programa deberia funcionar correctamente ahora.
echo.
pause
