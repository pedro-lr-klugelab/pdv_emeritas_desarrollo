# Guía Rápida de Comandos Docker - Farmacontrol PDV

## 🚀 Inicio Rápido

### Despliegue Completo (Automatizado)
```powershell
.\deploy-databases.ps1
```

### Despliegue Manual
```powershell
docker-compose down -v
docker-compose up -d
```

---

## 📦 Gestión de Contenedores

### Ver estado
```powershell
docker ps
docker-compose ps
```

### Iniciar contenedor
```powershell
docker-compose up -d
```

### Detener contenedor
```powershell
docker-compose down
```

### Detener y eliminar TODO (⚠️ Borra datos)
```powershell
docker-compose down -v
```

### Reiniciar contenedor
```powershell
docker-compose restart
```

### Ver logs
```powershell
# Logs en tiempo real
docker-compose logs -f mysql

# Últimas 100 líneas
docker-compose logs --tail=100 mysql

# Solo errores
docker-compose logs mysql 2>&1 | Select-String -Pattern "ERROR"
```

---

## 🗄️ Acceso a MySQL

### Conectarse a MySQL
```powershell
# Como usuario de la aplicación
docker exec -it farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123

# Como root
docker exec -it farmacontrol_mysql mysql -uroot -prootpassword
```

### Consultas rápidas desde PowerShell
```powershell
# Mostrar bases de datos
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "SHOW DATABASES;"

# Mostrar tablas de farmacontrol_global
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "USE farmacontrol_global; SHOW TABLES;"

# Mostrar tablas de farmacontrol_local
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "USE farmacontrol_local; SHOW TABLES;"

# Contar registros en una tabla
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "SELECT COUNT(*) FROM farmacontrol_global.empleados;"
```

---

## 💾 Backup y Restauración

### Backup Individual
```powershell
# Backup de farmacontrol_global
docker exec farmacontrol_mysql mysqldump -ufarmacontrol -pfarmacontrol123 farmacontrol_global > "backup_global_$(Get-Date -Format 'yyyyMMdd_HHmmss').sql"

# Backup de farmacontrol_local
docker exec farmacontrol_mysql mysqldump -ufarmacontrol -pfarmacontrol123 farmacontrol_local > "backup_local_$(Get-Date -Format 'yyyyMMdd_HHmmss').sql"
```

### Backup Completo
```powershell
# Backup de todas las bases de datos
docker exec farmacontrol_mysql mysqldump -ufarmacontrol -pfarmacontrol123 --databases farmacontrol_global farmacontrol_local mexico_local > "backup_completo_$(Get-Date -Format 'yyyyMMdd_HHmmss').sql"

# Backup completo incluyendo datos del sistema
docker exec farmacontrol_mysql mysqldump -ufarmacontrol -pfarmacontrol123 --all-databases > "backup_all_$(Get-Date -Format 'yyyyMMdd_HHmmss').sql"
```

### Restaurar desde Backup
```powershell
# Restaurar farmacontrol_global
docker exec -i farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 farmacontrol_global < backup_global.sql

# Restaurar farmacontrol_local
docker exec -i farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 farmacontrol_local < backup_local.sql

# Restaurar backup completo
docker exec -i farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 < backup_completo.sql
```

---

## 🔍 Diagnóstico y Solución de Problemas

### Verificar que MySQL esté respondiendo
```powershell
docker exec farmacontrol_mysql mysqladmin ping -h localhost -ufarmacontrol -pfarmacontrol123
```

### Ver información del servidor MySQL
```powershell
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "SELECT VERSION(); SHOW VARIABLES LIKE 'character_set%';"
```

### Ver procesos activos en MySQL
```powershell
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "SHOW PROCESSLIST;"
```

### Ver tamaño de las bases de datos
```powershell
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "SELECT table_schema AS 'Database', ROUND(SUM(data_length + index_length) / 1024 / 1024, 2) AS 'Size (MB)' FROM information_schema.tables GROUP BY table_schema;"
```

### Ver espacio usado por el contenedor
```powershell
docker system df
docker exec farmacontrol_mysql df -h
```

### Verificar logs de errores de MySQL
```powershell
docker exec farmacontrol_mysql tail -n 50 /var/log/mysql/error.log
```

---

## 🔧 Mantenimiento

### Optimizar tablas
```powershell
# Optimizar todas las tablas de farmacontrol_global
docker exec farmacontrol_mysql mysqlcheck -ufarmacontrol -pfarmacontrol123 --optimize farmacontrol_global

# Optimizar todas las tablas de farmacontrol_local
docker exec farmacontrol_mysql mysqlcheck -ufarmacontrol -pfarmacontrol123 --optimize farmacontrol_local
```

### Reparar tablas
```powershell
docker exec farmacontrol_mysql mysqlcheck -ufarmacontrol -pfarmacontrol123 --repair farmacontrol_global
docker exec farmacontrol_mysql mysqlcheck -ufarmacontrol -pfarmacontrol123 --repair farmacontrol_local
```

### Limpiar logs antiguos
```powershell
docker logs farmacontrol_mysql 2>&1 | Out-Null
docker-compose logs --no-log-prefix mysql | Out-File -FilePath "mysql_logs_$(Get-Date -Format 'yyyyMMdd').txt"
```

---

## 📊 Monitoreo

### Ver uso de recursos del contenedor
```powershell
# Uso de CPU y memoria en tiempo real
docker stats farmacontrol_mysql

# Solo una vez
docker stats --no-stream farmacontrol_mysql
```

### Ver conexiones activas
```powershell
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "SHOW STATUS LIKE 'Threads_connected';"
```

### Ver queries lentos
```powershell
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "SHOW VARIABLES LIKE 'slow_query%';"
```

---

## 🔄 Actualizaciones y Cambios

### Reiniciar después de cambios en docker-compose.yml
```powershell
docker-compose down
docker-compose up -d
```

### Recrear contenedor desde cero
```powershell
docker-compose down -v
docker-compose up -d
# ⚠️ Esto borra todos los datos y reimporta desde los archivos SQL
```

### Actualizar solo la configuración sin perder datos
```powershell
docker-compose down
docker-compose up -d
# Sin el flag -v, los datos persisten
```

---

## 🌐 Acceso Remoto (Si es necesario)

### Permitir acceso desde otra máquina
```powershell
# Modificar docker-compose.yml
# Cambiar:
#   ports:
#     - "127.0.0.1:3306:3306"  # Solo local
# Por:
#   ports:
#     - "3306:3306"  # Accesible desde red

# Luego reiniciar
docker-compose down
docker-compose up -d
```

### Conectarse desde otra máquina
```powershell
# Desde otra computadora en la misma red
mysql -h <IP_DEL_HOST> -P 3306 -ufarmacontrol -pfarmacontrol123 farmacontrol_global
```

---

## 🆘 Comandos de Emergencia

### El contenedor no inicia
```powershell
# Ver error detallado
docker-compose logs mysql

# Verificar puerto 3306
netstat -an | Select-String "3306"

# Detener servicio MySQL de Windows si existe
Stop-Service MySQL
```

### No puedo conectarme a MySQL
```powershell
# Verificar que el contenedor esté corriendo
docker ps

# Verificar logs
docker-compose logs mysql

# Reintentar conexión
docker exec -it farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123
```

### Resetear todo desde cero
```powershell
# 1. Detener y eliminar todo
docker-compose down -v

# 2. Eliminar volúmenes huérfanos
docker volume prune

# 3. Reiniciar Docker Desktop

# 4. Volver a desplegar
.\deploy-databases.ps1
```

---

## 📝 Información de Conexión

### Credenciales
```
Host:     localhost
Puerto:   3306
Usuario:  farmacontrol
Password: farmacontrol123
```

### Bases de Datos
```
- farmacontrol_global (Principal)
- farmacontrol_local (Local de sucursal)
- mexico_local (Catálogos)
```

---

## 📚 Más Información

- **Guía de instalación:** [README_DOCKER.md](README_DOCKER.md)
- **Detalles técnicos:** [README_CONFIGURACION_DOCKER.md](README_CONFIGURACION_DOCKER.md)
- **Resumen de cambios:** [CAMBIOS_DOCKER_DATABASE.md](CAMBIOS_DOCKER_DATABASE.md)
