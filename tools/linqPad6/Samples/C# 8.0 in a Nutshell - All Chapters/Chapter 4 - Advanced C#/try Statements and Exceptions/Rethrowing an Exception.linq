<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
</Query>

// Rethrowing lets you back out of handling an exception should circumstances turn out to be
// outside what you expected:

string s = null;

using (WebClient wc = new WebClient())
	try { s = wc.DownloadString ("http://www.albahari.com/nutshell/");  }
	catch (WebException ex)
	{
		if (ex.Status == WebExceptionStatus.NameResolutionFailure)
			Console.WriteLine ("Bad domain name");
		else
			throw;     // Can’t handle other sorts of WebException, so rethrow
	}

s.Dump();