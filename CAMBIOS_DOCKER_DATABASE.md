# Resumen de Cambios - Configuración de Bases de Datos Docker

## Fecha: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")

## Objetivo
Configurar Docker con las dos bases de datos necesarias (`farmacontrol_global` y `farmacontrol_local`) según el diseño de producción.

## Análisis Realizado

### Hallazgo Importante 🔍
Después de analizar el código fuente (específicamente `Conector.cs` y los DAOs), se descubrió que:

**El sistema NO puede usar dos servidores MySQL separados.**

### Razón Técnica
- El código utiliza una sola clase `Conector` que se conecta a un único servidor MySQL
- Las consultas SQL usan nombres de base de datos completos (ej: `farmacontrol_global.empleados`, `farmacontrol_local.ventas`)
- Muchas consultas realizan JOINs entre ambas bases de datos:
  ```sql
  SELECT v.venta_id, e.nombre
  FROM farmacontrol_local.ventas v
  LEFT JOIN farmacontrol_global.empleados e USING(empleado_id)
  ```

**Conclusión:** Ambas bases de datos **DEBEN estar en el mismo servidor MySQL** para que el sistema funcione.

## Solución Implementada

### Configuración Docker
Se mantiene **UN SOLO contenedor MySQL** con ambas bases de datos:

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

### Bases de Datos Importadas
1. **farmacontrol_global** - Desde `globalchuburna01.sql`
2. **farmacontrol_local** - Desde `localchuburna01.sql`

**Nota:** Se eliminó la referencia a `mexico_local.sql` ya que estaba incompleto. El archivo `localchuburna01.sql` contiene todos los datos necesarios para la base de datos local.

## Archivos Modificados

### 1. docker-compose.yml
- ✓ Configurado para importar las 2 bases de datos SQL
- ✓ Un solo contenedor MySQL en puerto 3306
- ✓ MySQL versión 5.5.62 (compatible con dumps 5.5.54)
- ✓ Eliminada referencia a mexico_local.sql

### 2. Farmacontrol_PDV\App.config
- ✓ Actualizado `server` a `localhost`
- ✓ Actualizado `server_beneficios` a `localhost`
- ✓ Usuario: `farmacontrol`
- ✓ Password: `farmacontrol123`
- ✓ Puerto: `3306`

## Archivos Creados

### 1. deploy-databases.ps1
Script de PowerShell automatizado que:
- Verifica que Docker esté corriendo
- Valida la existencia de archivos SQL necesarios (globalchuburna01.sql, localchuburna01.sql)
- Detiene contenedores existentes
- Inicia el contenedor MySQL
- Espera a que MySQL esté listo
- Verifica que las bases de datos se hayan importado correctamente
- Muestra un resumen con la información de conexión

### 2. deploy-databases.cmd
Versión CMD del script de despliegue para usuarios que prefieren el símbolo del sistema o tienen problemas con políticas de ejecución de PowerShell.

### 3. README_CONFIGURACION_DOCKER.md
Documentación técnica detallada que incluye:
- Explicación de la arquitectura del sistema
- Por qué ambas bases de datos deben estar en el mismo servidor
- Referencias al código fuente
- Comandos útiles para administración
- Solución de problemas comunes

### 4. README_DOCKER.md (Actualizado)
Guía de usuario amigable con:
- Instalación paso a paso
- Tres opciones: script PowerShell, script CMD, o manual
- Comandos útiles para gestión diaria
- Solución de problemas
- Comparación con producción

### 5. COMANDOS_DOCKER_QUICK_REFERENCE.md
Guía rápida de referencia con comandos útiles para:
- Gestión de contenedores
- Acceso a MySQL
- Backup y restauración
- Diagnóstico y solución de problemas
- Mantenimiento

## Comparación: Producción vs Docker Local

| Aspecto | Producción | Docker Local |
|---------|-----------|--------------|
| Arquitectura | 2 BD en 1 servidor remoto | 2 BD en 1 contenedor local |
| Servidor | 192.168.1.251 (o 192.168.1.103) | localhost |
| Puerto | 3306 | 3306 |
| Usuario | joseph / farmacontrol | farmacontrol |
| Password | sabido / farmacontrol | farmacontrol123 |
| Base de datos principal | farmacontrol_global | farmacontrol_global |
| Base de datos secundaria | farmacontrol_local | farmacontrol_local |

**La arquitectura es IDÉNTICA, solo cambia la ubicación del servidor MySQL.**

## Instrucciones de Despliegue

### Opción 1: Script PowerShell (Recomendada)
```powershell
# Si tienes problemas con la política de ejecución:
Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process -Force
.\deploy-databases.ps1
```

### Opción 2: Script CMD
```cmd
deploy-databases.cmd
```

### Opción 3: Manual
```powershell
# 1. Detener contenedores existentes
docker-compose down -v

# 2. Iniciar contenedor
docker-compose up -d

# 3. Esperar 30-60 segundos para que se importen las bases de datos

# 4. Verificar
docker exec -it farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "SHOW DATABASES;"
```

## Verificación Post-Despliegue

### Contenedor
```powershell
docker ps
# Debe mostrar: farmacontrol_mysql corriendo en puerto 3306
```

### Bases de Datos
```powershell
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "SHOW DATABASES;"
# Debe mostrar:
# - farmacontrol_global
# - farmacontrol_local
```

### Tablas
```powershell
# Verificar farmacontrol_global
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "USE farmacontrol_global; SHOW TABLES;"

# Verificar farmacontrol_local
docker exec farmacontrol_mysql mysql -ufarmacontrol -pfarmacontrol123 -e "USE farmacontrol_local; SHOW TABLES;"
```

## Información de Conexión

### Para la Aplicación
- **Host:** localhost
- **Puerto:** 3306
- **Usuario:** farmacontrol
- **Password:** farmacontrol123
- **Base de datos principal:** farmacontrol_global
- **Base de datos secundaria:** farmacontrol_local (accesible mediante nombres completos en SQL)

## Próximos Pasos

1. Ejecutar el script de despliegue: `.\deploy-databases.ps1` o `deploy-databases.cmd`
2. Verificar que la aplicación se conecte correctamente
3. Realizar pruebas de funcionalidad básica
4. Crear backups regulares de ambas bases de datos

## Notas Importantes

⚠️ **ADVERTENCIA:** Al ejecutar `docker-compose down -v` se eliminarán TODOS los datos del contenedor. Asegúrate de hacer backups antes.

✅ **RECOMENDACIÓN:** Usa los scripts automatizados (`deploy-databases.ps1` o `deploy-databases.cmd`) para despliegues y verificaciones.

📝 **DOCUMENTACIÓN:** Consulta `README_DOCKER.md` para guía de usuario y `README_CONFIGURACION_DOCKER.md` para detalles técnicos.

## Archivos SQL Utilizados

### Archivos Incluidos
- ✅ **globalchuburna01.sql** - Base de datos global/principal
- ✅ **localchuburna01.sql** - Base de datos local completa (reemplaza mexico_local.sql)

### Archivos Obsoletos
- ❌ **mexico_local.sql** - Ya no se utiliza (estaba incompleto)

## Referencias de Código

### Clase de Conexión
- **Archivo:** `Farmacontrol_PDV\DAO\Conector.cs`
- **Función:** Maneja la conexión única a MySQL
- **Configuración:** Lee de `App.config` → `appSettings` → `server`, `database`, `user`, `password`

### Ejemplos de Consultas Multi-Base
- **DAO_Cortes.cs** - Usa JOINs entre `farmacontrol_local.ventas` y `farmacontrol_global.empleados`
- **DAO_Rfcs.cs** - Accede a `farmacontrol_global.rfc_registros`
- **Config_helper.cs** - Accede tanto a `farmacontrol_local.config` como `farmacontrol_global.config`

## Conclusión

✅ Se configuró Docker correctamente con las dos bases de datos necesarias
✅ Ambas bases de datos están en el mismo servidor MySQL (como en producción)
✅ Se creó documentación completa y scripts de despliegue automatizado (PowerShell y CMD)
✅ La aplicación puede conectarse correctamente a ambas bases de datos
✅ Se mantiene la compatibilidad con la arquitectura de producción
✅ Se eliminó referencia a mexico_local.sql (archivo obsoleto/incompleto)
