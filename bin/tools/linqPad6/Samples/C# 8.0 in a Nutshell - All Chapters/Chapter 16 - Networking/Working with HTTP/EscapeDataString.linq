<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

string search = Uri.EscapeDataString ("(WebClient OR HttpClient)");
string language = Uri.EscapeDataString ("fr");
string requestURI = "http://www.google.com/search?q=" + search +
                    "&hl=" + language;

new WebClient { Proxy = null }.DownloadFile (requestURI, "results.html");
OpenHtml ("results.html");

void OpenHtml (string location)
{
	if (RuntimeInformation.IsOSPlatform (OSPlatform.Windows))
		Process.Start (new ProcessStartInfo ("cmd", $"/c start {location}"));
	else if (RuntimeInformation.IsOSPlatform (OSPlatform.Linux))
		Process.Start ("xdg-open", location); // Desktop Linux
	else throw new Exception ("Platform-specific code needed to open URL.");
}