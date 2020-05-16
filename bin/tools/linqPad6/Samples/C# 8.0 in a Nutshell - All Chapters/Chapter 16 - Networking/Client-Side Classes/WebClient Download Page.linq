<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

WebClient wc = new WebClient { Proxy = null };
wc.DownloadFile ("http://www.albahari.com/nutshell/code.aspx", "code.htm");

OpenHtml ("code.htm");

void OpenHtml (string location)
{
	if (RuntimeInformation.IsOSPlatform (OSPlatform.Windows))
		Process.Start (new ProcessStartInfo ("cmd", $"/c start {location}"));
	else if (RuntimeInformation.IsOSPlatform (OSPlatform.Linux))
		Process.Start ("xdg-open", location); // Desktop Linux
	else throw new Exception ("Platform-specific code needed to open URL.");
}