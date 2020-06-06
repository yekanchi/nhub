for /r %%N in (*.nupkg) do assets\nuget.exe push -Source "ITNugets" -ApiKey AzureDevOps %%N
pause

