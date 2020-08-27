$commonResult = E:\NHUB\bin\consoles\set_commons
if($env:clearlog -eq "1")
{
	clear;
}

Write-Color -Text 'Main Shell @CodeHub:',' pwsh_Console.bat' -Color Green, Blue;
cd E:\NHUB