

$RequiredModules = @("PSWriteColor","posh-git","oh-my-posh","IISAdministration","PowerShellGet","ComputerManagementDsc","NetworkingDsc","WindowsDefender","SqlServer","powershell-yaml","PSLogging","xNetworking", "TaskRunner","CredentialManager","ImportExcel","PSReadLine","WallpaperManager","newtonsoft.json","PoshRSJob","WinSCP","SharePointDSC","cChoco","FileContentDsc")



if((-Not (Get-PSRepository | Where-Object {$_.Name -eq $env:PSRepoName })) -Or ((Get-PSRepository -Name CodeHubPSRepo).SourceLocation -eq $env:PSRepoPath )) 
{
	UnRegister-PSRepository -Name $env:PSRepoName;
	Register-PSRepository -Name $env:PSRepoName  -SourceLocation $env:PSRepoPath -ScriptSourceLocation $env:PSRepoPath -InstallationPolicy Trusted;
	Write-Host "Registered" $env:PSRepoName ;
}

foreach ($moduleName in $RequiredModules) 
{
	if(-Not (Get-Module -ListAvailable -Name $moduleName))
	{
		Write-Host "Installing Module" $moduleName "From Repository:" $env:PSRepoName;
		if(Find-Module  -Name $moduleName -Repository $env:PSRepoName -ErrorAction SilentlyContinue)
		{			
			Write-Host $env:PSRepoName;
			Install-Module -Name $moduleName -Repository $env:PSRepoName;
		}
		else
		{
			Write-Host "Module:" $moduleName "Not Found At Repository:" $env:PSRepoName;
			Write-Host "Saving Module:" $moduleName "From Repository PSGallery";
			Save-Package -Name $moduleName -Provider NuGet -Source https://www.powershellgallery.com/api/v2 -Path $env:PSRepoPath;
			Install-Module -Name $moduleName -Repository $env:PSRepoName;
		}
	}
}