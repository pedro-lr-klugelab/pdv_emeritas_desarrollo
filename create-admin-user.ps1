# Script para crear o actualizar un usuario administrador

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Crear Usuario Administrador" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$username = Read-Host "Ingresa el nombre de usuario (ej: admin)"
$password = Read-Host "Ingresa la contraseña" -AsSecureString
$passwordPlain = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))

# Calcular MD5 hash de la contraseña
$md5 = New-Object -TypeName System.Security.Cryptography.MD5CryptoServiceProvider
$utf8 = New-Object -TypeName System.Text.UTF8Encoding
$hash = [System.BitConverter]::ToString($md5.ComputeHash($utf8.GetBytes($passwordPlain))).Replace("-", "").ToLower()

Write-Host ""
Write-Host "Creando usuario..." -ForegroundColor Yellow
Write-Host ""

# Crear usuario
$createUser = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
INSERT INTO empleados (nombre, username, password, activo, created, modified)
VALUES ('$username', '$username', '$hash', 1, NOW(), NOW())
ON DUPLICATE KEY UPDATE 
    password = '$hash',
    activo = 1,
    modified = NOW();
" 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Usuario creado/actualizado correctamente" -ForegroundColor Green
} else {
    Write-Host "✗ Error al crear usuario" -ForegroundColor Red
    Write-Host $createUser
    exit 1
}

# Obtener empleado_id
$empleadoId = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
SELECT empleado_id FROM empleados WHERE username = '$username';
" 2>&1 | Select-String -Pattern '\d+' | Select-Object -Last 1

Write-Host "✓ ID de empleado: $empleadoId" -ForegroundColor Green
Write-Host ""

# Obtener todos los módulos
Write-Host "Asignando todos los módulos..." -ForegroundColor Yellow
$modulos = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
SELECT modulo_id FROM modulos;
" 2>&1

# Asignar todos los módulos
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
DELETE FROM modulos_empleados WHERE empleado_id = $empleadoId;
INSERT INTO modulos_empleados (empleado_id, modulo_id, created, modified)
SELECT $empleadoId, modulo_id, NOW(), NOW()
FROM modulos;
" 2>&1

Write-Host "✓ Módulos asignados" -ForegroundColor Green
Write-Host ""

# Obtener todas las funciones
Write-Host "Asignando todas las funciones..." -ForegroundColor Yellow
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
DELETE FROM funciones_empleados WHERE empleado_id = $empleadoId;
INSERT INTO funciones_empleados (empleado_id, funcion_id, created, modified)
SELECT $empleadoId, funcion_id, NOW(), NOW()
FROM funciones;
" 2>&1

Write-Host "✓ Funciones asignadas" -ForegroundColor Green
Write-Host ""

# Verificar
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Usuario Administrador Creado" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Username: $username" -ForegroundColor White
Write-Host "Password: $passwordPlain" -ForegroundColor White
Write-Host ""

$permisos = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
SELECT 
    COUNT(DISTINCT me.modulo_id) as total_modulos,
    COUNT(DISTINCT fe.funcion_id) as total_funciones
FROM 
    empleados e
LEFT JOIN modulos_empleados me ON e.empleado_id = me.empleado_id
LEFT JOIN funciones_empleados fe ON e.empleado_id = fe.empleado_id
WHERE 
    e.empleado_id = $empleadoId;
" 2>&1

Write-Host $permisos
Write-Host ""
Write-Host "✓ Usuario listo para usar" -ForegroundColor Green
Write-Host ""
