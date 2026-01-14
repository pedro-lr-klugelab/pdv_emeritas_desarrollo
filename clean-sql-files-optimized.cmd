@echo off
setlocal enabledelayedexpansion

REM Script optimizado para limpiar archivos SQL grandes sin problemas de memoria

echo ========================================
echo   Limpiando Archivos SQL (Optimizado)
echo ========================================
echo.
echo Este proceso puede tomar 5-10 minutos por archivo...
echo Por favor, NO cierres esta ventana.
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

REM Función para limpiar un archivo
call :clean_file "globalchuburna01.sql"
if errorlevel 1 goto :error

call :clean_file "localchuburna01.sql"
if errorlevel 1 goto :error

echo.
echo ========================================
echo   Limpieza Completada
echo ========================================
echo.
echo Los archivos SQL han sido limpiados.
echo Se crearon archivos .backup por seguridad.
echo.
echo Ahora puedes ejecutar: import-now.cmd
echo.
pause
exit /b 0

:clean_file
setlocal
set "input_file=%~1"
set "backup_file=%~1.backup"
set "temp_file=%~1.tmp"
set "line_count=0"
set "removed_count=0"

echo ========================================
echo   Procesando %input_file%
echo ========================================
echo.

REM Crear backup si no existe
if not exist "%backup_file%" (
    echo Creando backup...
    copy /Y "%input_file%" "%backup_file%" >nul
    echo Backup creado
) else (
    echo Backup ya existe
)
echo.

echo Limpiando archivo (esto puede tomar varios minutos)...
echo.

REM Procesar archivo línea por línea
(
    for /f "usebackq delims=" %%a in ("%input_file%") do (
        set "line=%%a"
        set "skip=0"
        
        REM Verificar si la línea contiene comandos problemáticos
        echo !line! | findstr /I /C:"CHANGE MASTER TO" /C:"STOP SLAVE" /C:"START SLAVE" /C:"RESET SLAVE" /C:"SQL_LOG_BIN" /C:"GTID_PURGED" >nul
        if not errorlevel 1 (
            set "skip=1"
            set /a removed_count+=1
        )
        
        REM Escribir línea si no debe eliminarse
        if !skip!==0 (
            echo !line!
        )
        
        REM Contador de líneas (mostrar progreso cada 100000 líneas)
        set /a line_count+=1
        set /a mod=line_count %% 100000
        if !mod!==0 (
            echo Procesadas !line_count! lineas... 1>&2
        )
    )
) > "%temp_file%"

if errorlevel 1 (
    echo ERROR al procesar el archivo
    if exist "%temp_file%" del "%temp_file%"
    exit /b 1
)

REM Reemplazar archivo original
echo.
echo Finalizando...
move /Y "%temp_file%" "%input_file%" >nul

if errorlevel 1 (
    echo ERROR al reemplazar el archivo
    exit /b 1
)

echo.
echo Archivo procesado correctamente
echo Lineas procesadas: %line_count%
echo Lineas eliminadas: %removed_count%
echo.

endlocal
exit /b 0

:error
echo.
echo El proceso fallo.
pause
exit /b 1
