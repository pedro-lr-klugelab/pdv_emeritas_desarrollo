# Script para encontrar usuarios administradores en Farmacontrol PDV

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Búsqueda de Usuario Administrador" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Buscar empleados activos
Write-Host "Buscando empleados en la base de datos..." -ForegroundColor Yellow
Write-Host ""

$empleados = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
SELECT 
    e.empleado_id,
    e.nombre,
    e.username,
    e.activo,
    COUNT(DISTINCT me.modulo_id) as modulos_asignados,
    COUNT(DISTINCT fe.funcion_id) as funciones_asignadas
FROM 
    empleados e
LEFT JOIN 
    modulos_empleados me ON e.empleado_id = me.empleado_id
LEFT JOIN 
    funciones_empleados fe ON e.empleado_id = fe.empleado_id
WHERE 
    e.activo = 1
GROUP BY 
    e.empleado_id
ORDER BY 
    modulos_asignados DESC, funciones_asignadas DESC
LIMIT 10;
" 2>&1

Write-Host $empleados
Write-Host ""

# Buscar usuarios con más permisos
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Usuarios con Más Permisos" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$topUsers = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
SELECT 
    e.empleado_id,
    e.nombre,
    e.username,
    e.password,
    COUNT(DISTINCT me.modulo_id) as total_modulos,
    COUNT(DISTINCT fe.funcion_id) as total_funciones
FROM 
    empleados e
LEFT JOIN 
    modulos_empleados me ON e.empleado_id = me.empleado_id
LEFT JOIN 
    funciones_empleados fe ON e.empleado_id = fe.empleado_id
WHERE 
    e.activo = 1
GROUP BY 
    e.empleado_id
HAVING 
    total_modulos > 0 OR total_funciones > 0
ORDER BY 
    total_modulos DESC, total_funciones DESC
LIMIT 5;
" 2>&1

Write-Host $topUsers
Write-Host ""

# Mostrar módulos disponibles
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Módulos Disponibles en el Sistema" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$modulos = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
SELECT 
    modulo_id,
    nombre,
    descripcion
FROM 
    modulos
ORDER BY 
    nombre;
" 2>&1

Write-Host $modulos
Write-Host ""

# Buscar usuario "admin" o "administrador"
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Buscando Usuario 'admin' o similar" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$adminUsers = docker exec farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global -e "
SELECT 
    empleado_id,
    nombre,
    username,
    password,
    activo
FROM 
    empleados
WHERE 
    username LIKE '%admin%' 
    OR nombre LIKE '%admin%'
    OR username LIKE '%root%'
ORDER BY 
    empleado_id;
" 2>&1

if ($adminUsers -match "empleado_id") {
    Write-Host $adminUsers
} else {
    Write-Host "No se encontraron usuarios con 'admin' en el nombre" -ForegroundColor Yellow
}
Write-Host ""

# Resumen
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Resumen" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Para iniciar sesión en el programa, usa uno de los usuarios" -ForegroundColor White
Write-Host "con más módulos y funciones asignados." -ForegroundColor White
Write-Host ""
Write-Host "NOTA: Las contraseñas están encriptadas (hash MD5 o similar)." -ForegroundColor Yellow
Write-Host "Si no puedes iniciar sesión, puedes:" -ForegroundColor Yellow
Write-Host "  1. Crear un nuevo usuario administrador" -ForegroundColor Gray
Write-Host "  2. Resetear la contraseña de un usuario existente" -ForegroundColor Gray
Write-Host ""
