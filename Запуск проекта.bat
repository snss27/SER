@echo off
cd /d "%~dp0"

set ASPNETCORE_ENVIRONMENT=Development

REM START API
start "" /min cmd /c "cd Backend\SER.API && dotnet run"

REM START File Storage
start "" /min cmd /c "cd Backend\SER.FileStorage && dotnet run"

REM START Next.js
start "" /min cmd /c "cd Frontend\students-employment-records-site && npm run local_start"

echo Ready.
pause