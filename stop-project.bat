@echo off
echo Stopping...

taskkill /F /IM node.exe >nul 2>&1

taskkill /F /IM dotnet.exe >nul 2>&1

echo Success!
pause