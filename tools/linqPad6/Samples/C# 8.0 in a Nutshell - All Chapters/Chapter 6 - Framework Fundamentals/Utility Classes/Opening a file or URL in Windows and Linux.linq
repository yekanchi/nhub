<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

void Main()
{
	LaunchFileOrUrl ("http://www.albahari.com/nutshell");
}

void LaunchFileOrUrl (string url)
{
	if (RuntimeInformation.IsOSPlatform (OSPlatform.Linux))
		Process.Start ("xdg-open", url);
	else if (RuntimeInformation.IsOSPlatform (OSPlatform.Windows))
		Process.Start (new ProcessStartInfo (url) { UseShellExecute = true });
	else
		throw new NotSupportedException ("Platform unsupported.");
}