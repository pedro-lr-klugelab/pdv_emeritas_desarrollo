# ============================================
#  Terminal Payment Services Startup Script
#  Starts Java Simulator and TotalPos API
# ============================================

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  Terminal Payment Services Startup" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Set the base directory
$BaseDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$TerminalApiDir = Join-Path $BaseDir "integrations\terminal-api"
$SimulatorDir = Join-Path $TerminalApiDir "simulador\Servidor\main\webapp\WEB-INF\views"
$ApiDir = Join-Path $TerminalApiDir "src\TotalPosApi\bin\Debug\net48"

# Check if Java is available
$javaPath = Get-Command java -ErrorAction SilentlyContinue
if (-not $javaPath) {
    Write-Host "[ERROR] Java not found. Please install Java JDK 8 or higher." -ForegroundColor Red
    Write-Host "Download from: https://www.oracle.com/java/technologies/downloads/" -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host "[INFO] Java found: " -ForegroundColor Green
java -version 2>&1 | Select-Object -First 1
Write-Host ""

# Check if simulator JAR exists
$simulatorJar = Join-Path $SimulatorDir "Simulador-1.0.0-SNAPSHOT.jar"
if (-not (Test-Path $simulatorJar)) {
    Write-Host "[ERROR] Simulator JAR not found at:" -ForegroundColor Red
    Write-Host $simulatorJar -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

# Check if API executable exists
$apiExe = Join-Path $ApiDir "TotalPosApi.exe"
if (-not (Test-Path $apiExe)) {
    Write-Host "[ERROR] TotalPosApi.exe not found at:" -ForegroundColor Red
    Write-Host $apiExe -ForegroundColor Yellow
    Write-Host "Please build the project first using: dotnet build" -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

# Start Java Simulator
Write-Host "[INFO] Starting Java Simulator (BBVA Host) on port 8080..." -ForegroundColor Green
Start-Process -FilePath "cmd.exe" -ArgumentList "/k", "cd /d `"$SimulatorDir`" && java -jar Simulador-1.0.0-SNAPSHOT.jar" -WindowStyle Normal

# Wait for simulator to start
Write-Host "[INFO] Waiting for simulator to initialize..." -ForegroundColor Yellow
Start-Sleep -Seconds 5

# Start TotalPos API
Write-Host "[INFO] Starting TotalPos API on port 5077..." -ForegroundColor Green
Start-Process -FilePath "cmd.exe" -ArgumentList "/k", "cd /d `"$ApiDir`" && TotalPosApi.exe" -WindowStyle Normal

# Wait for API to start
Write-Host "[INFO] Waiting for API to initialize..." -ForegroundColor Yellow
Start-Sleep -Seconds 3

Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  Services Started Successfully!" -ForegroundColor Green
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "  - Java Simulator: http://localhost:8080" -ForegroundColor White
Write-Host "  - TotalPos API:   http://localhost:5077" -ForegroundColor White
Write-Host ""
Write-Host "  You can now start the Farmacontrol PDV application." -ForegroundColor White
Write-Host ""
Write-Host "  To stop services, close the terminal windows." -ForegroundColor Yellow
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Check if services are running
Write-Host "[INFO] Checking if services are running..." -ForegroundColor Yellow
try {
    $healthCheck = Invoke-RestMethod -Uri "http://localhost:5077/api/health" -Method GET -TimeoutSec 5 -ErrorAction SilentlyContinue
    Write-Host "[OK] TotalPos API is responding!" -ForegroundColor Green
} catch {
    Write-Host "[WARNING] TotalPos API may not be ready yet. Please wait a moment." -ForegroundColor Yellow
}

Read-Host "Press Enter to close this window"
