# Configuración de Bases de Datos con Docker

## Arquitectura del Sistema

El sistema **Farmacontrol PDV** utiliza **DOS bases de datos MySQL** que deben estar en el **mismo servidor**:

1. **farmacontrol_global** - Base de datos global/principal
2. **farmacontrol_local** - Base de datos local de la sucursal

### ¿Por qué en el mismo servidor?

El código utiliza una sola clase `Conector` que se conecta al servidor MySQL usando la configuración de `App.config`. Las consultas SQL referencian ambas bases de datos usando nombres completos:
- `farmacontrol_global.tabla`
- `farmacontrol_local.tabla`

Esto permite que una consulta pueda acceder a ambas bases de datos mediante JOINs y otras operaciones, pero **requiere que ambas estén en el mismo servidor MySQL**.

## Configuración Docker Actual

### Contenedor Único
```yaml
services:
  mysql:
    image: mysql:5.5.62
    container_name: farmacontrol_mysql
    ports:
      - "3306:3306"
    volumes:
      - ./globalchuburna01.sql:/docker-entrypoint-initdb.d/01-globalchuburna01.sql
      - ./localchuburna01.sql:/docker-entrypoint-initdb.d/02-localchuburna01.sql
```

### Bases de Datos Creadas
1. **farmacontrol_global** - Importada desde `globalchuburna01.sql`
2. **farmacontrol_local** - Importada desde `localchuburna01.sql`

## Configuración de Conexión

### App.config
```xml
<appSettings>
  <!-- Conexión única para ambas bases de datos -->
  <add key="server" value="localhost"/>
  <add key="database" value="farmacontrol_global"/>
  <add key="user" value="farmacontrol"/>
  <add key="password" value="farmacontrol123"/>
</appSettings>

<userSettings>
  <Farmacontrol_PDV.Properties.Configuracion>
    <!-- Configuración de conexión directa a BD -->
    <setting name="db_server" serializeAs="String">
      <value>localhost</value>
    </setting>
    <setting name="db_port" serializeAs="String">
      <value>3306</value>
    </setting>
    <setting name="db_user" serializeAs="String">
      <value>farmacontrol</value>
    </setting>
    <setting name="db_password" serializeAs="String">
      <value>farmacontrol123</value>
    </setting>
    <setting name="db_name" serializeAs="String">
      <value>farmacontrol_global</value>
    </setting>
  </Farmacontrol_PDV.Properties.Configuracion>
</userSettings>
```

## Instalación y Despliegue

### 1. Detener contenedores existentes (si existen)
```powershell
docker-compose down -v
```
**⚠️ ADVERTENCIA:** El flag `-v` elimina los volúmenes. Todos los datos se perderán.

### 2. Iniciar el contenedor con las bases de datos
```powershell
docker-compose up -d
```

### 3. Verificar la importación
```powershell
# Conectarse al contenedor
docker exec -it farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123

# Dentro de MySQL
SHOW DATABASES;
USE farmacontrol_global;
SHOW TABLES;
USE farmacontrol_local;
SHOW TABLES;
```

### 4. Verificar logs del contenedor
```powershell
docker-compose logs -f mysql
```

## Versiones de Software

- **MySQL:** 5.5.62
- **Character Set:** UTF-8
- **Collation:** utf8_general_ci

La versión MySQL 5.5.62 fue seleccionada porque coincide con la versión usada en los dumps SQL:
- `globalchuburna01.sql` - MySQL 5.5.54
- `localchuburna01.sql` - MySQL 5.5.54

## Credenciales de Acceso

### Usuario Aplicación
- **Usuario:** farmacontrol
- **Password:** farmacontrol123
- **Host:** localhost
- **Puerto:** 3306

### Usuario Root
- **Usuario:** root
- **Password:** rootpassword

## Comandos Útiles

### Backup de ambas bases de datos
```powershell
# Backup de farmacontrol_global
docker exec farmacontrol_mysql mysqldump -ufarmacontrol -pfarmacontrol123 farmacontrol_global > backup_global_$(Get-Date -Format "yyyyMMdd_HHmmss").sql

# Backup de farmacontrol_local
docker exec farmacontrol_mysql mysqldump -ufarmacontrol -pfarmacontrol123 farmacontrol_local > backup_local_$(Get-Date -Format "yyyyMMdd_HHmmss").sql
```

### Restaurar base de datos
```powershell
# Restaurar farmacontrol_global
docker exec -i farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 farmacontrol_global < backup_global.sql

# Restaurar farmacontrol_local
docker exec -i farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 farmacontrol_local < backup_local.sql
```

### Reiniciar contenedor
```powershell
docker-compose restart
```

### Ver estado del contenedor
```powershell
docker-compose ps
```

## Solución de Problemas

### Error: "Can't connect to MySQL server"
1. Verificar que Docker Desktop esté corriendo
2. Verificar que el contenedor esté activo: `docker ps`
3. Revisar logs: `docker-compose logs mysql`

### Error: "Access denied for user"
Verificar las credenciales en `App.config` coincidan con las configuradas en `docker-compose.yml`

### Puerto 3306 ya en uso
Si tienes otra instancia de MySQL corriendo:
```powershell
# Detener servicio MySQL local de Windows
Stop-Service MySQL

# O cambiar el puerto en docker-compose.yml
ports:
  - "3307:3306"  # Usar puerto 3307 en el host
```

### Datos no persisten después de reiniciar
Verificar que el volumen esté configurado correctamente:
```powershell
docker volume ls
docker volume inspect emerita_pdv-main_mysql_data
```

## Diferencias con Producción

### Producción Original
- **Server Global:** 192.168.1.251 (o 192.168.1.103)
- **Server Local:** 192.168.1.251 (mismo servidor)
- **Bases de datos:** Ambas en el mismo servidor MySQL remoto

### Entorno Docker Local
- **Server:** localhost
- **Bases de datos:** Ambas en el mismo contenedor Docker MySQL

La arquitectura es la misma, solo cambia la ubicación del servidor MySQL.

## Referencias de Código

El sistema utiliza la clase `Conector.cs` (ubicada en `Farmacontrol_PDV\DAO\Conector.cs`) que:
1. Lee la configuración del servidor desde `App.config`
2. Se conecta a `farmacontrol_global` como base de datos predeterminada
3. Permite ejecutar consultas que referencian ambas bases de datos usando nombres completos

Ejemplo de consulta multi-base de datos:
```sql
SELECT
    v.venta_id,
    e.nombre AS empleado
FROM
    farmacontrol_local.ventas v
LEFT JOIN 
    farmacontrol_global.empleados e USING(empleado_id)
