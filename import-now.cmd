@echo off
REM Script rapido de importacion con force
REM Ignora errores de replicacion y continua con la importacion

echo ========================================
echo   Importando Bases de Datos
echo ========================================
echo.
echo NOTA: Usando --force para ignorar errores de replicacion
echo Esto tomara 20-30 minutos aproximadamente
echo No cierres esta ventana durante el proceso
echo.

REM Importar farmacontrol_global
echo ========================================
echo Importando farmacontrol_global...
echo Inicio: %TIME%
echo ========================================
echo.

docker exec -i farmacontrol_mysql mysql -uroot -prootpassword --force farmacontrol_global < globalchuburna01.sql

echo.
echo Fin: %TIME%
echo farmacontrol_global importada
echo (Errores de replicacion ignorados automaticamente)
echo.

REM Verificar tablas en farmacontrol_global
echo Verificando tablas...
docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SELECT COUNT(*) as total FROM information_schema.tables WHERE table_schema='farmacontrol_global';"
echo.

REM Importar farmacontrol_local
echo ========================================
echo Importando farmacontrol_local...
echo Inicio: %TIME%
echo ========================================
echo.

docker exec -i farmacontrol_mysql mysql -uroot -prootpassword --force farmacontrol_local < localchuburna01.sql

echo.
echo Fin: %TIME%
echo farmacontrol_local importada
echo (Errores de replicacion ignorados automaticamente)
echo.

REM Verificar tablas en farmacontrol_local
echo Verificando tablas...
docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SELECT COUNT(*) as total FROM information_schema.tables WHERE table_schema='farmacontrol_local';"
echo.

echo ========================================
echo   IMPORTACION COMPLETADA
echo ========================================
echo.
echo Las bases de datos estan listas para usar.
echo.
pause
exit /b 0
