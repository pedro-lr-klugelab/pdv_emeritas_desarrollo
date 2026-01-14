# Script optimizado para limpiar archivos SQL grandes
# Procesa los archivos línea por línea para evitar problemas de memoria

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Limpiando Archivos SQL (Optimizado)" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

function Clean-SqlFile {
    param(
        [string]$inputFile,
        [string]$outputFile
    )
    
    Write-Host "Procesando $inputFile..." -ForegroundColor Yellow
    Write-Host "  Este proceso puede tomar 2-5 minutos..." -ForegroundColor Gray
    
    # Patrones a eliminar
    $patterns = @(
        "^-- CHANGE MASTER TO",
        "^CHANGE MASTER TO",
        "^STOP SLAVE",
        "^START SLAVE",
        "^RESET SLAVE",
        "^SET @@SESSION.SQL_LOG_BIN",
        "^SET @@GLOBAL.GTID_PURGED"
    )
    
    # Crear archivo temporal
    $tempFile = "$outputFile.tmp"
    
    try {
        # Abrir archivo de lectura
        $reader = [System.IO.File]::OpenText($inputFile)
        # Abrir archivo de escritura
        $writer = [System.IO.File]::CreateText($tempFile)
        
        $lineCount = 0
        $removedCount = 0
        
        while ($null -ne ($line = $reader.ReadLine())) {
            $lineCount++
            
            # Mostrar progreso cada 100,000 líneas
            if ($lineCount % 100000 -eq 0) {
                Write-Host "  Procesadas $lineCount líneas..." -ForegroundColor Gray
            }
            
            # Verificar si la línea debe eliminarse
            $shouldRemove = $false
            foreach ($pattern in $patterns) {
                if ($line -match $pattern) {
                    $shouldRemove = $true
                    $removedCount++
                    break
                }
            }
            
            # Escribir línea si no debe eliminarse
            if (-not $shouldRemove) {
                $writer.WriteLine($line)
            }
        }
        
        # Cerrar archivos
        $reader.Close()
        $writer.Close()
        
        # Reemplazar archivo original
        Move-Item -Path $tempFile -Destination $outputFile -Force
        
        Write-Host "  ✓ Procesadas $lineCount líneas" -ForegroundColor Green
        Write-Host "  ✓ Eliminadas $removedCount líneas problemáticas" -ForegroundColor Green
        
    } catch {
        Write-Host "  ✗ Error: $_" -ForegroundColor Red
        if (Test-Path $tempFile) {
            Remove-Item $tempFile -Force
        }
        throw
    }
}

$archivos = @("globalchuburna01.sql", "localchuburna01.sql")

foreach ($archivo in $archivos) {
    if (-not (Test-Path $archivo)) {
        Write-Host "✗ No se encuentra: $archivo" -ForegroundColor Red
        continue
    }
    
    # Crear backup
    $backup = "$archivo.backup"
    if (-not (Test-Path $backup)) {
        Write-Host "Creando backup de $archivo..." -ForegroundColor Yellow
        Copy-Item $archivo $backup
        Write-Host "✓ Backup creado: $backup" -ForegroundColor Green
    } else {
        Write-Host "✓ Backup ya existe: $backup" -ForegroundColor Green
    }
    Write-Host ""
    
    # Limpiar archivo
    try {
        Clean-SqlFile -inputFile $archivo -outputFile $archivo
        Write-Host "✓ $archivo procesado correctamente" -ForegroundColor Green
    } catch {
        Write-Host "✗ Error al procesar $archivo" -ForegroundColor Red
        Write-Host "Puedes restaurar desde el backup: $backup" -ForegroundColor Yellow
    }
    Write-Host ""
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Limpieza Completada" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Los archivos SQL han sido limpiados y están listos para importar." -ForegroundColor Green
Write-Host "Se crearon archivos .backup por seguridad." -ForegroundColor Yellow
Write-Host ""
Write-Host "Ahora puedes ejecutar: .\import-now.ps1" -ForegroundColor Cyan
Write-Host ""
