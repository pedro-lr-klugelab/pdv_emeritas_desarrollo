# Instalación de Bases de Datos con Docker

## ⚠️ IMPORTANTE: Arquitectura del Sistema

El sistema Farmacontrol PDV utiliza **DOS bases de datos** que **DEBEN estar en el mismo servidor MySQL**:
- **farmacontrol_global** - Base de datos global/principal (importada desde `globalchuburna01.sql`)
- **farmacontrol_local** - Base de datos local de la sucursal (importada desde `localchuburna01.sql`)

**¿Por qué en el mismo servidor?** El código usa nombres de base de datos completos en las consultas SQL (ej: `farmacontrol_global.empleados`, `farmacontrol_local.ventas`), lo que permite JOINs entre ambas bases de datos.

## Requisitos Previos
- Docker Desktop instalado y en ejecución
- Archivos SQL en el directorio raíz del proyecto:
  - `globalchuburna01.sql`
  - `localchuburna01.sql`

## Instalación Rápida (Recomendado)

### Opción 1: Script Automatizado (PowerShell)

Ejecuta el script de PowerShell que configura todo automáticamente:

```powershell
# Si tienes problemas con la política de ejecución:
Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process -Force
.\deploy-databases.ps1
```

### Opción 2: Script Automatizado (CMD)

Si prefieres usar el símbolo del sistema:

```cmd
deploy-databases.cmd
```

Estos scripts:
- ✓ Verifican que Docker esté corriendo
- ✓ Verifican que existan los archivos SQL necesarios
- ✓ Detienen contenedores existentes
- ✓ Inician el contenedor MySQL
- ✓ Esperan a que MySQL esté listo
- ✓ Verifican que las bases de datos se hayan importado correctamente

### Opción 3: Manual

1. Detener contenedores existentes (si existen):
```powershell
docker-compose down -v
```

2. Iniciar el contenedor MySQL:
```powershell
docker-compose up -d
```

3. Esperar a que MySQL esté listo (puede tomar 30-60 segundos):
```powershell
docker-compose logs -f mysql
# Presiona Ctrl+C cuando veas "mysqld: ready for connections"
```

Los archivos SQL se importarán automáticamente al iniciar el contenedor por primera vez en este orden:
1. `globalchuburna01.sql` - Base de datos global
2. `localchuburna01.sql` - Base de datos local

## Verificación de la Instalación

1. Conectarse al contenedor MySQL:
```powershell
docker exec -it farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123
```

2. Verificar las bases de datos:
```sql
SHOW DATABASES;
-- Deberías ver: farmacontrol_global, farmacontrol_local

USE farmacontrol_global;
SHOW TABLES;

USE farmacontrol_local;
SHOW TABLES;

EXIT;
```

## Credenciales de Acceso

### Para la Aplicación
- **Host:** localhost
- **Puerto:** 3306
- **Base de datos principal:** farmacontrol_global
- **Usuario:** farmacontrol
- **Password:** farmacontrol123

### Usuario Root (Solo para Administración)
- **Usuario:** root
- **Password:** rootpassword

## Configuración de la Aplicación

El archivo `App.config` ya está configurado para conectarse a Docker:

```xml
<appSettings>
  <add key="server" value="localhost"/>
  <add key="database" value="farmacontrol_global"/>
  <add key="user" value="farmacontrol"/>
  <add key="password" value="farmacontrol123"/>
</appSettings>
```

**Nota:** Aunque la conexión apunta a `farmacontrol_global`, las consultas SQL pueden acceder a ambas bases de datos usando nombres completos.

## Comandos Útiles

### Gestión del Contenedor

```powershell
# Detener el contenedor
docker-compose down

# Reiniciar el contenedor
docker-compose restart

# Ver logs en tiempo real
docker-compose logs -f mysql

# Ver estado del contenedor
docker-compose ps

# Detener y eliminar TODO (incluyendo datos) - ¡CUIDADO!
docker-compose down -v
```

### Backup de Bases de Datos

```powershell
# Backup de farmacontrol_global
docker exec farmacontrol_mysql mysqldump -ufarmacontrol -pfarmacontrol123 farmacontrol_global > backup_global_$(Get-Date -Format "yyyyMMdd_HHmmss").sql

# Backup de farmacontrol_local
docker exec farmacontrol_mysql mysqldump -ufarmacontrol -pfarmacontrol123 farmacontrol_local > backup_local_$(Get-Date -Format "yyyyMMdd_HHmmss").sql

# Backup de ambas bases de datos
docker exec farmacontrol_mysql mysqldump -ufarmacontrol -pfarmacontrol123 --databases farmacontrol_global farmacontrol_local > backup_completo_$(Get-Date -Format "yyyyMMdd_HHmmss").sql
```

### Restaurar desde Backup

```powershell
# Restaurar farmacontrol_global
docker exec -i farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 farmacontrol_global < backup_global.sql

# Restaurar farmacontrol_local
docker exec -i farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 farmacontrol_local < backup_local.sql
```

## Información Técnica

### Versiones de Software
- **MySQL:** 5.5.62
- **Character Set:** utf8
- **Collation:** utf8_general_ci

La versión MySQL 5.5.62 coincide con la versión de los dumps originales (MySQL 5.5.54).

### Estructura del Contenedor
```yaml
services:
  mysql:
    image: mysql:5.5.62
    container_name: farmacontrol_mysql
    ports:
      - "3306:3306"
    volumes:
      - mysql_data:/var/lib/mysql  # Persistencia de datos
      - ./globalchuburna01.sql:/docker-entrypoint-initdb.d/01-globalchuburna01.sql
      - ./localchuburna01.sql:/docker-entrypoint-initdb.d/02-localchuburna01.sql
```

### Bases de Datos Creadas
| Base de Datos | Fuente | Descripción |
|---------------|--------|-------------|
| farmacontrol_global | globalchuburna01.sql | Base de datos global/principal |
| farmacontrol_local | localchuburna01.sql | Base de datos local de sucursal |

## Solución de Problemas

### El puerto 3306 ya está en uso

Si tienes otro MySQL corriendo localmente:

**Opción 1:** Detener el servicio MySQL de Windows
```powershell
Stop-Service MySQL
```

**Opción 2:** Cambiar el puerto en `docker-compose.yml`
```yaml
ports:
  - "3307:3306"  # Usar puerto 3307 en el host
```

Luego actualiza el `App.config`:
```xml
<setting name="db_port" serializeAs="String">
  <value>3307</value>
</setting>
```

### Error: "Can't connect to MySQL server"

1. Verificar que Docker Desktop esté corriendo
2. Verificar que el contenedor esté activo:
   ```powershell
   docker ps
   ```
3. Revisar logs para ver errores:
   ```powershell
   docker-compose logs mysql
   ```

### MySQL se detiene inmediatamente

Revisa los logs para identificar el error:
```powershell
docker-compose logs mysql
```

Errores comunes:
- Archivos SQL con errores de sintaxis
- Falta de permisos en el directorio de volúmenes
- Conflicto con otro servicio en el puerto 3306

### Bases de datos no se importan

1. Verifica que los archivos SQL existan en el directorio raíz
2. Los archivos se importan solo la primera vez que se crea el contenedor
3. Para reimportar, elimina el volumen y vuelve a crear el contenedor:
   ```powershell
   docker-compose down -v
   docker-compose up -d
   ```

### Error de caracteres UTF-8

El contenedor está configurado para UTF-8. Si tienes problemas, verifica que los archivos SQL estén codificados en UTF-8.

### Los datos no persisten después de reiniciar

Verifica que el volumen esté configurado correctamente:
```powershell
docker volume ls
docker volume inspect emerita_pdv-main_mysql_data
```

### Error: Script PowerShell no se ejecuta

Si obtienes error de política de ejecución:
```powershell
Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process -Force
.\deploy-databases.ps1
```

O usa la versión CMD:
```cmd
deploy-databases.cmd
```

## Diferencias con Producción

| Aspecto | Producción | Docker Local |
|---------|-----------|--------------|
| Servidor Global | 192.168.1.251 | localhost:3306 |
| Servidor Local | 192.168.1.251 | localhost:3306 |
| Ubicación | Mismo servidor remoto | Mismo contenedor local |
| Usuario | joseph | farmacontrol |
| Password | sabido | farmacontrol123 |

**Nota:** La arquitectura es idéntica (ambas bases de datos en el mismo servidor), solo cambia la ubicación.

## Más Información

Para detalles técnicos sobre cómo el código accede a las bases de datos, consulta:
- [README_CONFIGURACION_DOCKER.md](README_CONFIGURACION_DOCKER.md) - Arquitectura y referencias de código
- [COMANDOS_DOCKER_QUICK_REFERENCE.md](COMANDOS_DOCKER_QUICK_REFERENCE.md) - Guía rápida de comandos
- [Conector.cs](Farmacontrol_PDV/DAO/Conector.cs) - Clase de conexión a base de datos
