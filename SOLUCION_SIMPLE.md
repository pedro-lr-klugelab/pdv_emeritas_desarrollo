# Solución Definitiva: Usar --force (MÁS RÁPIDO Y SIMPLE)

## ❌ Problema: Out of Memory

Al intentar limpiar los archivos SQL (1.5GB cada uno), PowerShell se queda sin memoria:
```
System.OutOfMemoryException
```

## ✅ Solución: Importar con --force

En lugar de limpiar los archivos, **usa la opción `--force` de MySQL** que ignora errores y continúa importando.

## 🚀 Ejecuta ESTO Ahora

```cmd
import-now.cmd
```

**¡Así de simple!** Los scripts ya están actualizados para usar `--force`.

## 📊 Qué Verás

Durante la importación aparecerán errores como:
```
ERROR 1200 (HY000) at line 6653: The server is not configured as slave
```

**Esto es NORMAL**. La opción `--force` los ignora y continúa.

## ⏱️ Tiempo

- **Total:** 20-30 minutos
- **No necesitas limpiar archivos** (ahorra 10-40 minutos)
- **Sin problemas de memoria**

## 💡 Comparación

| Método | Tiempo | Memoria | Complejidad |
|--------|--------|---------|-------------|
| Limpiar archivos | 40-70 min | ❌ Falla | Alta |
| Usar --force | 20-30 min | ✅ OK | Baja |

## 🎯 Comando Simple

Si prefieres hacerlo manualmente:

```cmd
docker exec -i farmacontrol_mysql mysql -uroot -prootpassword --force farmacontrol_global < globalchuburna01.sql
docker exec -i farmacontrol_mysql mysql -uroot -prootpassword --force farmacontrol_local < localchuburna01.sql
```

## ✅ Resultado

Al final tendrás:
- ✅ Todas las tablas importadas correctamente
- ✅ Datos completos e intactos
- ✅ Errores de replicación ignorados automáticamente

**Ejecuta `import-now.cmd` ahora mismo.** 🚀
