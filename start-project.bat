@echo off
echo Starting site...
start /b cmd /c "cd Frontend\students-employment-records-site && npm run dev

echo Starting API (port 44377)...
start /b cmd /c "cd Backend\SER.API && dotnet run --no-launch-profile --urls=https://localhost:44377

echo Success!
pause
