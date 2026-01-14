# Script corregido para crear usuario administrador
# Primero detecta la estructura de la tabla

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Crear Usuario Administrador (v2)" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Detectar estructura de la tabla empleados
Write-Host "Detectando estructura de la tabla empleados..." -ForegroundColor Yellow
$structure = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "DESCRIBE empleados;" 2>&1

Write-Host $structure
Write-Host ""

# Ver un empleado de ejemplo
Write-Host "Viendo empleado de ejemplo..." -ForegroundColor Yellow
$ejemplo = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT * FROM empleados LIMIT 1;" 2>&1

Write-Host $ejemplo
Write-Host ""

# Detectar nombre de columna de usuario
$userColumn = "usuario"
if ($structure -match "username") {
    $userColumn = "username"
} elseif ($structure -match "login") {
    $userColumn = "login"
} elseif ($structure -match "user") {
    $userColumn = "user"
}

Write-Host "Columna de usuario detectada: $userColumn" -ForegroundColor Green
Write-Host ""

# Solicitar datos
$username = Read-Host "Ingresa el nombre de usuario (ej: admin)"
$password = Read-Host "Ingresa la contraseña" -AsSecureString
$passwordPlain = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))
$nombre = Read-Host "Ingresa el nombre completo (ej: Administrador del Sistema)"

# Calcular MD5 hash de la contraseña
$md5 = New-Object -TypeName System.Security.Cryptography.MD5CryptoServiceProvider
$utf8 = New-Object -TypeName System.Text.UTF8Encoding
$hash = [System.BitConverter]::ToString($md5.ComputeHash($utf8.GetBytes($passwordPlain))).Replace("-", "").ToLower()

Write-Host ""
Write-Host "Creando usuario..." -ForegroundColor Yellow
Write-Host ""

# Verificar si el usuario ya existe
$existingUser = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT empleado_id FROM empleados WHERE $userColumn = '$username';" 2>&1 | Select-String -Pattern '\d+'

if ($existingUser) {
    Write-Host "Usuario '$username' ya existe. Actualizando..." -ForegroundColor Yellow
    
    # Actualizar usuario existente
    docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
    UPDATE empleados 
    SET password = '$hash',
        nombre = '$nombre',
        activo = 1,
        modified = NOW()
    WHERE $userColumn = '$username';
    " 2>&1
    
    $empleadoId = $existingUser.ToString().Trim()
} else {
    Write-Host "Creando nuevo usuario..." -ForegroundColor Yellow
    
    # Crear nuevo usuario
    docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
    INSERT INTO empleados (nombre, $userColumn, password, activo, created, modified)
    VALUES ('$nombre', '$username', '$hash', 1, NOW(), NOW());
    " 2>&1
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "✗ Error al crear usuario" -ForegroundColor Red
        exit 1
    }
    
    # Obtener empleado_id
    $empleadoId = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "SELECT LAST_INSERT_ID();" 2>&1 | Select-String -Pattern '\d+' | Select-Object -Last 1
    $empleadoId = $empleadoId.ToString().Trim()
}

Write-Host "✓ Usuario creado/actualizado correctamente" -ForegroundColor Green
Write-Host "✓ ID de empleado: $empleadoId" -ForegroundColor Green
Write-Host ""

# Asignar todos los módulos
Write-Host "Asignando todos los módulos..." -ForegroundColor Yellow
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
DELETE FROM modulos_empleados WHERE empleado_id = $empleadoId;
INSERT INTO modulos_empleados (empleado_id, modulo_id, created, modified)
SELECT $empleadoId, modulo_id, NOW(), NOW()
FROM modulos;
" 2>&1

Write-Host "✓ Módulos asignados" -ForegroundColor Green
Write-Host ""

# Asignar todas las funciones
Write-Host "Asignando todas las funciones..." -ForegroundColor Yellow
docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
DELETE FROM funciones_empleados WHERE empleado_id = $empleadoId;
INSERT INTO funciones_empleados (empleado_id, funcion_id, created, modified)
SELECT $empleadoId, funcion_id, NOW(), NOW()
FROM funciones;
" 2>&1

Write-Host "✓ Funciones asignadas" -ForegroundColor Green
Write-Host ""

# Verificar permisos
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Usuario Administrador Configurado" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Nombre: $nombre" -ForegroundColor White
Write-Host "Usuario: $username" -ForegroundColor White
Write-Host "Password: $passwordPlain" -ForegroundColor White
Write-Host "ID: $empleadoId" -ForegroundColor White
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

Write-Host "Permisos asignados:" -ForegroundColor White
Write-Host $permisos
Write-Host ""
Write-Host "✓ Usuario listo para usar en el sistema" -ForegroundColor Green
Write-Host ""
