<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
</Query>

WebClient wc = new WebClient { Proxy = null };
try
{
	string s = wc.DownloadString ("http://www.albahari.com/notthere");
}
catch (WebException ex)
{
	if (ex.Status == WebExceptionStatus.NameResolutionFailure)
		Console.WriteLine ("Bad domain name");
	else if (ex.Status == WebExceptionStatus.ProtocolError)
	{
		HttpWebResponse response = (HttpWebResponse)ex.Response;
		Console.WriteLine (response.StatusDescription);      // "Not Found"
		if (response.StatusCode == HttpStatusCode.NotFound)
			Console.WriteLine ("Not there!");                  // "Not there!"
	}
	else throw;
}