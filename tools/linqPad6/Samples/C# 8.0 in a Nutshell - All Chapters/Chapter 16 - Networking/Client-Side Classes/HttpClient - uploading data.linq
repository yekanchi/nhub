<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
</Query>

var client = new HttpClient (new HttpClientHandler { UseProxy = false });
var request = new HttpRequestMessage (
	HttpMethod.Post, "http://www.albahari.com/EchoPost.aspx");
request.Content = new StringContent ("This is a test");
HttpResponseMessage response = await client.SendAsync (request);
response.EnsureSuccessStatusCode();
Console.WriteLine (await response.Content.ReadAsStringAsync());