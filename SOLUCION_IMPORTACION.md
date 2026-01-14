# Solución al Problema de Importación de Bases de Datos

## Problema Detectado

Las bases de datos no se estaban importando correctamente debido a dos razones principales:

1. **Archivos SQL muy grandes**: Los archivos pesan ~1.5GB cada uno
   - `globalchuburna01.sql` - 1558.8 MB
   - `localchuburna01.sql` - 1476.66 MB

2. **Limitaciones de docker-entrypoint-initdb.d**: Este método tiene problemas con archivos SQL grandes y complejos

## Solución Implementada

### Nuevo Enfoque: Importación Manual Post-Inicio

En lugar de usar `docker-entrypoint-initdb.d` para importar los dumps grandes, ahora:

1. **Docker solo crea las bases de datos** (usando `00-init-databases.sql`)
2. **Los scripts importan manualmente** los archivos SQL después de que MySQL esté listo

### Ventajas de este Enfoque

✅ **Soporte para archivos grandes**: No hay límite de tamaño  
✅ **Mejor manejo de errores**: Podemos detectar y reportar problemas  
✅ **Feedback en tiempo real**: Se muestra el progreso de cada importación  
✅ **Verificación post-importación**: Contamos las tablas para confirmar  

## Configuración Actualizada

### docker-compose.yml
```yaml
services:
  mysql:
    image: mysql:5.5.62
    volumes:
      - ./00-init-databases.sql:/docker-entrypoint-initdb.d/00-init-databases.sql
      # Los dumps grandes NO se montan aquí
    command: --character-set-server=utf8 --collation-server=utf8_general_ci --max_allowed_packet=512M
```

**Nota importante**: Se agregó `--max_allowed_packet=512M` para soportar queries grandes en los dumps.

### Proceso de Importación

Los scripts ahora ejecutan:

```powershell
# 1. Iniciar contenedor y esperar a que MySQL esté listo
docker-compose up -d

# 2. Importar farmacontrol_global
docker exec -i farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global < globalchuburna01.sql

# 3. Importar farmacontrol_local
docker exec -i farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local < localchuburna01.sql

# 4. Verificar tablas importadas
```

## Pasos para Resolver el Problema

### Opción 1: Usar los Scripts Actualizados (Recomendado)

1. **Detener y eliminar el contenedor actual:**
   ```powershell
   docker-compose down -v
   ```

2. **Ejecutar el script de despliegue actualizado:**
   
   **PowerShell:**
   ```powershell
   Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process -Force
   .\deploy-databases.ps1
   ```
   
   **CMD:**
   ```cmd
   deploy-databases.cmd
   ```

⏱️ **Tiempo estimado**: 20-30 minutos para importar ambas bases de datos

### Opción 2: Manual

1. **Detener y eliminar contenedor:**
   ```powershell
   docker-compose down -v
   ```

2. **Iniciar contenedor:**
   ```powershell
   docker-compose up -d
   ```

3. **Esperar a que MySQL esté listo (30 segundos):**
   ```powershell
   Start-Sleep -Seconds 30
   ```

4. **Importar farmacontrol_global:**
   ```powershell
   docker exec -i farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_global < globalchuburna01.sql
   ```
   ⏱️ Esto tomará 10-15 minutos

5. **Importar farmacontrol_local:**
   ```powershell
   docker exec -i farmacontrol_mysql mysql -uroot -prootpassword farmacontrol_local < localchuburna01.sql
   ```
   ⏱️ Esto tomará 10-15 minutos

6. **Verificar:**
   ```powershell
   docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "USE farmacontrol_global; SHOW TABLES;"
   docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "USE farmacontrol_local; SHOW TABLES;"
   ```

## Lo Que Hacen los Scripts Actualizados

### deploy-databases.ps1 (PowerShell)

1. ✓ Verifica que Docker esté corriendo
2. ✓ Verifica archivos SQL y muestra su tamaño
3. ✓ Detiene contenedores existentes con `docker-compose down -v`
4. ✓ Inicia contenedor MySQL
5. ✓ Espera a que MySQL esté listo
6. ✓ **Importa globalchuburna01.sql** con barra de progreso
7. ✓ Verifica tablas en farmacontrol_global
8. ✓ **Importa localchuburna01.sql** con barra de progreso
9. ✓ Verifica tablas en farmacontrol_local
10. ✓ Muestra resumen con número exacto de tablas

### deploy-databases.cmd (CMD)

Mismo proceso pero para el símbolo del sistema de Windows.

## Verificación de Logs

Si algo falla durante la importación:

```powershell
# Ver logs del contenedor
docker-compose logs mysql

# Ver logs en tiempo real durante la importación
docker-compose logs -f mysql
```

## Errores Comunes y Soluciones

### Error: "Got a packet bigger than 'max_allowed_packet' bytes"
- **Causa:** Query muy grande en el dump
- **Solución:** Ya configurado con `--max_allowed_packet=512M`

### Error: "Lost connection to MySQL server during query"
- **Causa:** Timeout en conexión durante importación larga
- **Solución:** El script ahora usa `docker exec` que no tiene timeout

### Error: Importación se congela
- **Causa:** Archivo SQL corrupto o muy grande
- **Solución:** 
  1. Verificar integridad de archivos SQL
  2. Revisar logs: `docker-compose logs mysql`
  3. Aumentar memoria de Docker Desktop

### Error: "Access denied"
- **Causa:** Usuario sin permisos
- **Solución:** Scripts usan usuario root para importación

### Pocas tablas importadas
- **Causa:** Importación anterior incompleta
- **Solución:** **SIEMPRE** usar `docker-compose down -v` antes de reintentar

## Archivos Necesarios

✅ **00-init-databases.sql** - Crea las bases de datos vacías  
✅ **globalchuburna01.sql** - Dump de farmacontrol_global (1558 MB)  
✅ **localchuburna01.sql** - Dump de farmacontrol_local (1476 MB)  
✅ **docker-compose.yml** - Configuración del contenedor  
❌ **01-use-local.sql** - Ya no se necesita (eliminado)

## Notas Importantes

⚠️ **CRÍTICO**: Siempre ejecutar `docker-compose down -v` antes de reimportar  
- El flag `-v` elimina el volumen con datos anteriores  
- Sin esto, los datos viejos permanecen y la importación falla  

⏱️ **PACIENCIA**: La importación de 3GB de datos toma tiempo  
- Espera 10-15 minutos por cada base de datos  
- No interrumpas el proceso  
- Los scripts mostrarán progreso  

💾 **ESPACIO EN DISCO**: Asegúrate de tener al menos 10GB libres  
- 3GB para archivos SQL  
- ~6GB para bases de datos importadas  
- Overhead de Docker  

🔄 **PERSISTENCIA**: Una vez importado correctamente  
- Los datos persisten entre reinicios del contenedor  
- Solo se pierden con `docker-compose down -v`  

## Comandos Útiles Durante la Importación

### Monitorear progreso
```powershell
# Ver procesos activos en MySQL
docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SHOW PROCESSLIST;"

# Ver tamaño actual de las bases de datos
docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SELECT table_schema AS 'Database', ROUND(SUM(data_length + index_length) / 1024 / 1024, 2) AS 'Size (MB)' FROM information_schema.tables GROUP BY table_schema;"
```

### Verificar tablas importadas
```powershell
# Contar tablas en farmacontrol_global
docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'farmacontrol_global';"

# Contar tablas en farmacontrol_local
docker exec farmacontrol_mysql mysql -uroot -prootpassword -e "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'farmacontrol_local';"
```

## Próximos Pasos

1. ☑️ Ejecutar `docker-compose down -v`
2. ☑️ Ejecutar `.\deploy-databases.ps1` o `deploy-databases.cmd`
3. ☑️ Esperar 20-30 minutos a que se completen las importaciones
4. ☑️ Verificar que el script reporte el número correcto de tablas
5. ☑️ Conectar la aplicación y probar funcionalidad

## Comparación: Antes vs Ahora

| Aspecto | Antes | Ahora |
|---------|-------|-------|
| Método | docker-entrypoint-initdb.d | Importación manual |
| Archivos grandes | ❌ Problemas | ✅ Funciona |
| Progreso visible | ❌ No | ✅ Sí |
| Tiempo estimado | Desconocido | 20-30 minutos |
| Verificación | Manual | ✅ Automática |
| Manejo de errores | ❌ Pobre | ✅ Excelente |
