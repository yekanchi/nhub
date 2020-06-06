for /F "tokens=*" %%A in (PackagesNames.txt) do assets\nuget.exe install %%A -OutputDirectory .\assets\temp
powershell.exe -executionpolicy bypass -file .\assets\ps1Scripts\copyPackages.ps1
powershell.exe -executionpolicy bypass -file .\assets\ps1Scripts\removeAllFiles.ps1
pause