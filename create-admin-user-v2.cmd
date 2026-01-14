@echo off
setlocal enabledelayedexpansion

echo ========================================
echo   Crear Usuario Administrador (v2)
echo ========================================
echo.

REM Detectar estructura de la tabla
echo Detectando estructura de la tabla empleados...
echo.
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "DESCRIBE empleados;"
echo.

REM Ver ejemplo
echo Viendo empleado de ejemplo...
echo.
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT * FROM empleados LIMIT 1\G"
echo.

REM Solicitar datos
set /p username="Ingresa el nombre de usuario (ej: admin): "
set /p password="Ingresa la contraseña: "
set /p nombre="Ingresa el nombre completo (ej: Administrador Sistema): "

REM Calcular MD5 (usando PowerShell)
echo.
echo Calculando hash de contraseña...
for /f %%i in ('powershell -Command "$md5 = New-Object -TypeName System.Security.Cryptography.MD5CryptoServiceProvider; $utf8 = New-Object -TypeName System.Text.UTF8Encoding; [System.BitConverter]::ToString($md5.ComputeHash($utf8.GetBytes('%password%'))).Replace('-', '').ToLower()"') do set hash=%%i

echo Hash: %hash%
echo.

REM Detectar nombre de columna (intentar con usuario primero)
echo Creando/Actualizando usuario...
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "INSERT INTO empleados (nombre, usuario, password, activo, created, modified) VALUES ('%nombre%', '%username%', '%hash%', 1, NOW(), NOW()) ON DUPLICATE KEY UPDATE password = '%hash%', nombre = '%nombre%', activo = 1, modified = NOW();" 2>nul

if errorlevel 1 (
    REM Si falla con 'usuario', intenta con 'username'
    docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "INSERT INTO empleados (nombre, username, password, activo, created, modified) VALUES ('%nombre%', '%username%', '%hash%', 1, NOW(), NOW()) ON DUPLICATE KEY UPDATE password = '%hash%', nombre = '%nombre%', activo = 1, modified = NOW();"
)

echo Usuario creado/actualizado
echo.

REM Obtener empleado_id
echo Obteniendo ID de empleado...
for /f "skip=1" %%i in ('docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT empleado_id FROM empleados WHERE usuario = '%username%' OR username = '%username%' LIMIT 1;"') do set empleado_id=%%i

echo ID de empleado: %empleado_id%
echo.

REM Asignar módulos
echo Asignando todos los modulos...
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "DELETE FROM modulos_empleados WHERE empleado_id = %empleado_id%; INSERT INTO modulos_empleados (empleado_id, modulo_id, created, modified) SELECT %empleado_id%, modulo_id, NOW(), NOW() FROM modulos;"

echo Modulos asignados
echo.

REM Asignar funciones
echo Asignando todas las funciones...
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "DELETE FROM funciones_empleados WHERE empleado_id = %empleado_id%; INSERT INTO funciones_empleados (empleado_id, funcion_id, created, modified) SELECT %empleado_id%, funcion_id, NOW(), NOW() FROM funciones;"

echo Funciones asignadas
echo.

REM Verificar
echo ========================================
echo   Usuario Administrador Configurado
echo ========================================
echo.
echo Nombre: %nombre%
echo Usuario: %username%
echo Password: %password%
echo ID: %empleado_id%
echo.

echo Verificando permisos...
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT COUNT(DISTINCT me.modulo_id) as total_modulos, COUNT(DISTINCT fe.funcion_id) as total_funciones FROM empleados e LEFT JOIN modulos_empleados me ON e.empleado_id = me.empleado_id LEFT JOIN funciones_empleados fe ON e.empleado_id = fe.empleado_id WHERE e.empleado_id = %empleado_id%;"

echo.
echo Usuario listo para usar en el sistema
echo.
pause
