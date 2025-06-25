@echo off

echo Stopping dotnet-services...
taskkill /F /IM dotnet.exe >nul 2>&1

echo Stopping Next.js...
taskkill /F /IM node.exe >nul 2>&1

echo Completed.
pause