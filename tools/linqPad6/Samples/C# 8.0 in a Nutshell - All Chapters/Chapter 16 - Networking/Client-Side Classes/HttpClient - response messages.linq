<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
</Query>

var client = new HttpClient();
// The GetAsync method also accepts a CancellationToken.
HttpResponseMessage response = await client.GetAsync ("http://www.linqpad.net");
response.EnsureSuccessStatusCode();
string html = await response.Content.ReadAsStringAsync();
