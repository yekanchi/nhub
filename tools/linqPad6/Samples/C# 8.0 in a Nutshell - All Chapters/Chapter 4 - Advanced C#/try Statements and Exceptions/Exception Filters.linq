<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
</Query>

try
{
	new WebClient().DownloadString ("http://thisDoesNotExist");
}
catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
{
	"Timeout!".Dump();
}
catch (WebException ex) when (ex.Status == WebExceptionStatus.NameResolutionFailure)
{
	"Name resolution failure!".Dump();
}
catch (WebException ex) 
{
	$"Some other failure: {ex.Status}".Dump();
}