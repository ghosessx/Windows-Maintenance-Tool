@echo off
setlocal
title Building System Maintenance Tool...
echo ===============================================================================
echo                Building Lightweight System Maintenance Tool
echo ===============================================================================
echo.

set CSC_PATH=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe
if not exist "%CSC_PATH%" set CSC_PATH=C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe

if not exist "%CSC_PATH%" (
    echo [ERROR] Microsoft .NET Framework C# Compiler csc.exe not found!
    echo Please ensure .NET Framework 4.5+ is enabled.
    pause
    exit /b 1
)

echo [*] Compiler found: %CSC_PATH%
echo [*] Compiling source code...

"%CSC_PATH%" /target:winexe /optimize+ /platform:anycpu /reference:System.dll,System.Drawing.dll,System.Windows.Forms.dll,System.Core.dll /win32manifest:"src\app.manifest" /win32icon:"src\app.ico" /out:"SystemMaintenance.exe" "src\Program.cs"

if %errorlevel% neq 0 (
    echo.
    echo [!] Build failed!
    pause
    exit /b %errorlevel%
)

echo.
echo ===============================================================================
echo [+] BUILD SUCCESSFUL!
echo [+] Executable created: SystemMaintenance.exe
echo ===============================================================================
echo.
pause
