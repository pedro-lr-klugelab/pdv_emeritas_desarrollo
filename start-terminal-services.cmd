@echo off
REM ============================================
REM  Terminal Payment Services Startup Script
REM  Starts Java Simulator and TotalPos API
REM ============================================

echo ============================================
echo  Terminal Payment Services Startup
echo ============================================
echo.

REM Set the base directory (where this script is located)
set "BASE_DIR=%~dp0"
set "TERMINAL_API_DIR=%BASE_DIR%integrations\terminal-api"
set "SIMULATOR_DIR=%TERMINAL_API_DIR%\simulador\Servidor\main\webapp\WEB-INF\views"
set "API_DIR=%TERMINAL_API_DIR%\src\TotalPosApi\bin\Debug\net48"

REM Check if Java is available
where java >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Java not found. Please install Java JDK 8 or higher.
    echo Download from: https://www.oracle.com/java/technologies/downloads/
    pause
    exit /b 1
)

echo [INFO] Java found: 
java -version 2>&1 | findstr /i "version"
echo.

REM Check if simulator JAR exists
if not exist "%SIMULATOR_DIR%\Simulador-1.0.0-SNAPSHOT.jar" (
    echo [ERROR] Simulator JAR not found at:
    echo %SIMULATOR_DIR%\Simulador-1.0.0-SNAPSHOT.jar
    pause
    exit /b 1
)

REM Check if API executable exists
if not exist "%API_DIR%\TotalPosApi.exe" (
    echo [ERROR] TotalPosApi.exe not found at:
    echo %API_DIR%\TotalPosApi.exe
    echo Please build the project first using: dotnet build
    pause
    exit /b 1
)

echo [INFO] Starting Java Simulator (BBVA Host) on port 8080...
echo.
start "BBVA Simulator" cmd /k "cd /d "%SIMULATOR_DIR%" && java -jar Simulador-1.0.0-SNAPSHOT.jar"

REM Wait a few seconds for simulator to start
echo [INFO] Waiting for simulator to initialize...
timeout /t 5 /nobreak >nul

echo [INFO] Starting TotalPos API on port 5077...
echo.
start "TotalPos API" cmd /k "cd /d "%API_DIR%" && TotalPosApi.exe"

REM Wait for API to start
echo [INFO] Waiting for API to initialize...
timeout /t 3 /nobreak >nul

echo.
echo ============================================
echo  Services Started Successfully!
echo ============================================
echo.
echo  - Java Simulator: http://localhost:8080
echo  - TotalPos API:   http://localhost:5077
echo.
echo  You can now start the Farmacontrol PDV application.
echo.
echo  To stop services, close the terminal windows.
echo ============================================
echo.

pause
