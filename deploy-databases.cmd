@echo off
REM Script de Despliegue de Bases de Datos Farmacontrol
REM Este script configura y despliega las bases de datos MySQL en Docker

echo ========================================
echo   Farmacontrol PDV - Despliegue MySQL
echo ========================================
echo.

REM Verificar archivos SQL necesarios
echo Verificando archivos SQL necesarios...
if not exist "00-init-databases.sql" (
    echo ERROR: No se encuentra 00-init-databases.sql
    goto :error
)
if not exist "globalchuburna01.sql" (
    echo ERROR: No se encuentra globalchuburna01.sql
    goto :error
)
if not exist "localchuburna01.sql" (
    echo ERROR: No se encuentra localchuburna01.sql
    goto :error
)
if not exist "docker-compose.yml" (
    echo ERROR: No se encuentra docker-compose.yml
    goto :error
)
echo Todos los archivos encontrados
echo.

REM Advertencia
echo ADVERTENCIA: Este proceso eliminara cualquier contenedor y datos existentes.
echo Si tienes datos importantes, haz un backup antes de continuar.
echo.
echo NOTA: La importacion de archivos grandes puede tomar 10-20 minutos.
echo.
set /p continuar="Deseas continuar? (S/N): "
if /i not "%continuar%"=="S" (
    echo Operacion cancelada por el usuario.
    goto :end
)
echo.

REM Detener y eliminar contenedores existentes
echo Deteniendo contenedores existentes...
docker-compose down -v >nul 2>&1
echo Contenedores detenidos y volumenes eliminados
echo.

REM Iniciar el contenedor
echo Iniciando contenedor MySQL...
docker-compose up -d

if errorlevel 1 (
    echo.
    echo ERROR: Fallo al iniciar el contenedor.
    echo Revisa los logs con: docker-compose logs mysql
    goto :error
)

echo Contenedor iniciado
echo.

REM Esperar a que MySQL este listo
echo Esperando a que MySQL este listo...
timeout /t 5 /nobreak >nul

:wait_loop
docker exec farmacontrol_mysql mysqladmin ping -h localhost -uroot -prootpassword >nul 2>&1
if errorlevel 1 (
    echo .
    timeout /t 2 /nobreak >nul
    goto :wait_loop
)

echo.
echo MySQL esta listo
echo.

REM Verificar creacion de bases de datos
echo Verificando creacion de bases de datos...
docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SHOW DATABASES;" 2>nul | findstr /C:"farmacontrol_global" >nul
if errorlevel 1 (
    echo ERROR: No se crearon las bases de datos
    goto :error
)
echo Bases de datos creadas correctamente
echo.

REM Importar farmacontrol_global
echo ========================================
echo Importando farmacontrol_global...
echo Esto puede tomar 10-15 minutos...
echo ========================================
echo.

docker exec -i farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global < globalchuburna01.sql

if errorlevel 1 (
    echo ERROR al importar farmacontrol_global
    echo Revisa los logs con: docker-compose logs mysql
    goto :error
)

echo farmacontrol_global importada correctamente
echo.

REM Verificar tablas en farmacontrol_global
echo Verificando tablas en farmacontrol_global...
docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "USE farmacontrol_global; SHOW TABLES;" 2>nul | find /c /v "" > temp_count.txt
set /p num_tablas_global=<temp_count.txt
del temp_count.txt
set /a num_tablas_global=%num_tablas_global%-1
echo Tablas en farmacontrol_global: %num_tablas_global%
echo.

REM Importar farmacontrol_local
echo ========================================
echo Importando farmacontrol_local...
echo Esto puede tomar 10-15 minutos...
echo ========================================
echo.

docker exec -i farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local < localchuburna01.sql

if errorlevel 1 (
    echo ERROR al importar farmacontrol_local
    echo Revisa los logs con: docker-compose logs mysql
    goto :error
)

echo farmacontrol_local importada correctamente
echo.

REM Verificar tablas en farmacontrol_local
echo Verificando tablas en farmacontrol_local...
docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "USE farmacontrol_local; SHOW TABLES;" 2>nul | find /c /v "" > temp_count.txt
set /p num_tablas_local=<temp_count.txt
del temp_count.txt
set /a num_tablas_local=%num_tablas_local%-1
echo Tablas en farmacontrol_local: %num_tablas_local%
echo.

REM Resumen final
echo ========================================
echo          DESPLIEGUE COMPLETADO
echo ========================================
echo.
echo Informacion de conexion:
echo   Host:     localhost
echo   Puerto:   3306
echo   Usuario:  farmacontrol
echo   Password: farmacontrol123
echo.
echo Bases de datos disponibles:
echo   - farmacontrol_global (%num_tablas_global% tablas)
echo   - farmacontrol_local (%num_tablas_local% tablas)
echo.
echo Comandos utiles:
echo   Ver logs:       docker-compose logs -f mysql
echo   Detener:        docker-compose down
echo   Reiniciar:      docker-compose restart
echo   Conectar MySQL: docker exec -it farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123
echo.
echo La aplicacion ahora puede conectarse a las bases de datos.
echo.
goto :end

:error
echo.
echo El despliegue fallo. Revisa los errores anteriores.
pause
exit /b 1

:end
pause
exit /b 0
