LINQPad6.exe is framework-dependent, so you must install .NET Core 3.1 or .NET Core 3.0.

The .NET Core Runtime isn't enough: you need DESKTOP RUNTIME - or the SDK.

*** You can check that it's installed by running "Download .NET Core 3.exe" ***

You should end up with the following folders:
   C:\Program Files\dotnet\shared\Microsoft.NETCore.App
   C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App

NON-STANDARD NETCORE INSTALLATIONS
==================================

You can install or xcopy .NET Core to a non-standard location, such as c:\users\joe\dotnet.
This is handy if you don't have admin rights to your PC.

If you do this, be sure to set the DOTNET_ROOT environment variable to the folder containing dotnet.exe.
For example:

   set DOTNET_ROOT c:\users\joe\dotnet
=================
www.downloadly.ir