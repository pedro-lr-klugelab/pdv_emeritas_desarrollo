@echo off
REM Script para limpiar archivos SQL de comandos de replicación

echo ========================================
echo   Limpiando Archivos SQL
echo ========================================
echo.
echo Este proceso puede tomar unos minutos...
echo.

REM Verificar que existen los archivos
if not exist "globalchuburna01.sql" (
    echo ERROR: No se encuentra globalchuburna01.sql
    goto :error
)
if not exist "localchuburna01.sql" (
    echo ERROR: No se encuentra localchuburna01.sql
    goto :error
)

REM Crear backups si no existen
if not exist "globalchuburna01.sql.backup" (
    echo Creando backup de globalchuburna01.sql...
    copy /Y globalchuburna01.sql globalchuburna01.sql.backup >nul
    echo Backup creado
    echo.
)

if not exist "localchuburna01.sql.backup" (
    echo Creando backup de localchuburna01.sql...
    copy /Y localchuburna01.sql localchuburna01.sql.backup >nul
    echo Backup creado
    echo.
)

echo ========================================
echo   Procesando globalchuburna01.sql
echo ========================================
echo.
echo Eliminando comandos de replicacion...
powershell -Command "(Get-Content 'globalchuburna01.sql' -Raw) -replace 'CHANGE MASTER TO.*?;', '' -replace 'STOP SLAVE;', '' -replace 'START SLAVE;', '' -replace 'RESET SLAVE;', '' -replace 'SET @@SESSION\.SQL_LOG_BIN.*?;', '' -replace 'SET @@GLOBAL\.GTID_PURGED.*?;', '' | Set-Content 'globalchuburna01.sql' -Encoding UTF8"

if errorlevel 1 (
    echo ERROR al procesar globalchuburna01.sql
    goto :error
)
echo globalchuburna01.sql procesado
echo.

echo ========================================
echo   Procesando localchuburna01.sql
echo ========================================
echo.
echo Eliminando comandos de replicacion...
powershell -Command "(Get-Content 'localchuburna01.sql' -Raw) -replace 'CHANGE MASTER TO.*?;', '' -replace 'STOP SLAVE;', '' -replace 'START SLAVE;', '' -replace 'RESET SLAVE;', '' -replace 'SET @@SESSION\.SQL_LOG_BIN.*?;', '' -replace 'SET @@GLOBAL\.GTID_PURGED.*?;', '' | Set-Content 'localchuburna01.sql' -Encoding UTF8"

if errorlevel 1 (
    echo ERROR al procesar localchuburna01.sql
    goto :error
)
echo localchuburna01.sql procesado
echo.

echo ========================================
echo   Limpieza Completada
echo ========================================
echo.
echo Los archivos SQL han sido limpiados y estan listos para importar.
echo Se crearon archivos .backup por seguridad.
echo.
echo Ahora puedes ejecutar: import-now.cmd
echo.
pause
exit /b 0

:error
echo.
echo El proceso fallo.
pause
exit /b 1
