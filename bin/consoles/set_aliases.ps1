Write-Host "Setting Aliases...";

function global:Run-Verdaccio { verdaccio -l 2222 -c "$env:CHUB_HOME\bin\libs\verdaccio\config.yaml" }
Set-Alias -Name "verd" -Value Run-Verdaccio -Scope Global;

function global:Get-GitLog { git log --all --decorate --oneline --graph --abbrev-commit --pretty=format:'%Cred%h%Creset -%C(yellow)%d%Creset %s %Cgreen(%cr) %C(bold blue)<%an>%Creset' };
Set-Alias -Name 'pgl' -Value Get-GitLog -Scope Global;

Write-Host "Aliases Are Set";