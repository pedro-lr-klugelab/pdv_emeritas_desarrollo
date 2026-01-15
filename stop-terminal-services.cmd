@echo off
REM ============================================
REM  Terminal Payment Services Stop Script
REM  Stops Java Simulator and TotalPos API
REM ============================================

echo ============================================
echo  Stopping Terminal Payment Services
echo ============================================
echo.

echo [INFO] Stopping TotalPos API...
taskkill /IM TotalPosApi.exe /F 2>nul
if %ERRORLEVEL% EQU 0 (
    echo [OK] TotalPosApi.exe stopped.
) else (
    echo [INFO] TotalPosApi.exe was not running.
)

echo.
echo [INFO] Stopping Java Simulator...
REM Find and kill java processes running the simulator
for /f "tokens=2" %%i in ('wmic process where "commandline like '%%Simulador-1.0.0-SNAPSHOT.jar%%'" get processid 2^>nul ^| findstr /r "[0-9]"') do (
    taskkill /PID %%i /F 2>nul
)

echo.
echo ============================================
echo  Services Stopped
echo ============================================
echo.

pause
