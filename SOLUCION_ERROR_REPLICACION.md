# Solución al Error de Replicación MySQL

## 🔴 Error Encontrado

```
ERROR 1200 (HY000) at line 6653: The server is not configured as slave; 
fix in config file or with CHANGE MASTER TO
```

## 📋 Causa del Problema

Los archivos SQL (`globalchuburna01.sql` y `localchuburna01.sql`) contienen **comandos de replicación MySQL** que fueron generados con `mysqldump` desde un servidor configurado como esclavo (slave) en una configuración de replicación master-slave.

Estos comandos incluyen:
- `CHANGE MASTER TO` - Configuración de replicación
- `STOP SLAVE` / `START SLAVE` - Control de replicación
- `SET @@SESSION.SQL_LOG_BIN` - Control de logging binario
- `SET @@GLOBAL.GTID_PURGED` - Control de transacciones GTID

## ✅ Solución: Limpiar Archivos SQL

He creado scripts que automáticamente eliminan estos comandos problemáticos de los archivos SQL.

### Paso 1: Limpiar los Archivos SQL

**PowerShell:**
```powershell
Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process -Force
.\clean-sql-files.ps1
```

**CMD:**
```cmd
clean-sql-files.cmd
```

### Paso 2: Importar las Bases de Datos

Una vez limpiados los archivos, ejecuta la importación:

**PowerShell:**
```powershell
.\import-now.ps1
```

**CMD:**
```cmd
import-now.cmd
```

## 🔄 Proceso Completo (Desde Cero)

Si quieres empezar completamente desde cero:

```powershell
# 1. Detener contenedor y eliminar volumen
docker-compose down -v

# 2. Limpiar archivos SQL
.\clean-sql-files.ps1

# 3. Desplegar todo
.\deploy-databases.ps1
```

**O con CMD:**
```cmd
REM 1. Detener contenedor y eliminar volumen
docker-compose down -v

REM 2. Limpiar archivos SQL
clean-sql-files.cmd

REM 3. Desplegar todo
deploy-databases.cmd
```

## 📝 Qué Hacen los Scripts de Limpieza

1. **Crean backup** de los archivos originales (`.backup`)
2. **Eliminan comandos de replicación**:
   - `CHANGE MASTER TO`
   - `STOP SLAVE` / `START SLAVE`
   - `RESET SLAVE`
3. **Eliminan comandos GTID**:
   - `SET @@SESSION.SQL_LOG_BIN`
   - `SET @@GLOBAL.GTID_PURGED`
4. **Guardan versión limpia** lista para importar

## ⚠️ Notas Importantes

### Los Backups Son Automáticos
- Los scripts crean automáticamente archivos `.backup`
- Solo se crea el backup la primera vez
- Si algo sale mal, puedes restaurar desde el backup

### Los Archivos Son Grandes
- El proceso de limpieza puede tomar 1-2 minutos
- Cada archivo es ~1.5GB
- Se procesarán en memoria

### No Afecta los Datos
- Solo elimina comandos de configuración
- Los datos (INSERT, CREATE TABLE, etc.) permanecen intactos
- Es completamente seguro

## 🎯 Resumen de Comandos

### Opción 1: Solo Limpiar e Importar (El contenedor ya está corriendo)

```cmd
clean-sql-files.cmd
import-now.cmd
```

### Opción 2: Empezar Desde Cero

```cmd
docker-compose down -v
clean-sql-files.cmd
deploy-databases.cmd
```

## 🔍 Verificar Si Necesitas Limpiar

Para ver si tus archivos SQL tienen comandos de replicación:

```powershell
# Buscar comandos problemáticos
Select-String -Path "globalchuburna01.sql" -Pattern "CHANGE MASTER|STOP SLAVE|START SLAVE"
Select-String -Path "localchuburna01.sql" -Pattern "CHANGE MASTER|STOP SLAVE|START SLAVE"
```

Si encuentra coincidencias, definitivamente necesitas ejecutar el script de limpieza.

## 💡 Por Qué Ocurre Esto

Los archivos SQL fueron generados con `mysqldump` desde un servidor que tenía replicación activa. El comando probablemente fue algo como:

```bash
mysqldump --master-data --single-transaction farmacontrol_global > globalchuburna01.sql
```

La opción `--master-data` incluye información de replicación que no es necesaria (y causa errores) cuando se importa a un servidor independiente.

## 📊 Tiempo Estimado

- **Limpieza de archivos:** 1-2 minutos
- **Importación farmacontrol_global:** 10-15 minutos
- **Importación farmacontrol_local:** 10-15 minutos
- **Total:** ~25-35 minutos

## ✅ Siguiente Paso

Ejecuta el script de limpieza ahora:

```cmd
clean-sql-files.cmd
```

Luego ejecuta la importación:

```cmd
import-now.cmd
```

¡Y listo! 🎉
