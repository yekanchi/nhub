<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
</Query>

var cc = new CookieContainer();

var request = (HttpWebRequest)WebRequest.Create ("http://www.google.com");
request.Proxy = null;
request.CookieContainer = cc;
using (var response = (HttpWebResponse)request.GetResponse())
{
	foreach (Cookie c in response.Cookies)
	{
		Console.WriteLine (" Name:   " + c.Name);
		Console.WriteLine (" Value:  " + c.Value);
		Console.WriteLine (" Path:   " + c.Path);
		Console.WriteLine (" Domain: " + c.Domain);
	}
	// Read response stream...
}