
#posh-git
if (-not (Get-Module -ListAvailable -Name posh-git)) {
    Write-Host "posh-git Is not Instealled";
	Write-Host "Installing posh-git";
	Install-Module posh-git -Scope CurrentUser -Force -SkipPublisherCheck
}

#oh-my-posh
if (-not (Get-Module -ListAvailable -Name oh-my-posh)) {
    Write-Host  "oh-my-posh Is not Instealled";
	Write-Host "Installing oh-my-posh";
	Install-Module oh-my-posh -Scope CurrentUser -Force -SkipPublisherCheck
}