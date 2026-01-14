@echo off
echo ========================================
echo   Resetear Contrasena Usuario JAJA
echo ========================================
echo.
echo Usuario ROOT encontrado: JAJA (LEVI JOSAPHAT)
echo   - es_root: SI
echo   - es_admin: SI
echo   - Modulos: 20/64
echo   - Funciones: 6/24
echo.
echo Este usuario tiene maximos privilegios en el sistema.
echo.

set /p nueva_password="Ingresa la nueva contrasena para JAJA: "

echo.
echo Calculando hash MD5...
for /f %%i in ('powershell -Command "$md5 = New-Object -TypeName System.Security.Cryptography.MD5CryptoServiceProvider; $utf8 = New-Object -TypeName System.Text.UTF8Encoding; [System.BitConverter]::ToString($md5.ComputeHash($utf8.GetBytes('%nueva_password%'))).Replace('-', '').ToLower()"') do set hash=%%i

echo Hash: %hash%
echo.

echo Actualizando contrasena...
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "UPDATE empleados SET password = '%hash%', activo = 1, puede_login = 1 WHERE usuario = 'JAJA';"

if errorlevel 1 (
    echo ERROR al actualizar contrasena
    pause
    exit /b 1
)

echo.
echo ========================================
echo   Contrasena Actualizada
echo ========================================
echo.
echo Usuario: JAJA
echo Password: %nueva_password%
echo.
echo Ahora puedes iniciar sesion en el programa.
echo.
pause
