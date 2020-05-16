<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

WebClient wc = new WebClient { Proxy = null };
wc.QueryString.Add ("q", "WebClient");     // Search for "WebClient"
wc.QueryString.Add ("hl", "fr");           // Display page in French
wc.DownloadFile ("http://www.google.com/search", "results.html");
OpenHtml ("results.html");

void OpenHtml (string location)
{
	if (RuntimeInformation.IsOSPlatform (OSPlatform.Windows))
		Process.Start (new ProcessStartInfo ("cmd", $"/c start {location}"));
	else if (RuntimeInformation.IsOSPlatform (OSPlatform.Linux))
		Process.Start ("xdg-open", location); // Desktop Linux
	else throw new Exception ("Platform-specific code needed to open URL.");
}