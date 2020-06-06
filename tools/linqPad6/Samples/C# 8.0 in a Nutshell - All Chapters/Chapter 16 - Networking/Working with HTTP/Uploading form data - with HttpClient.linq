<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

string uri = "http://www.albahari.com/EchoPost.aspx";
var client = new HttpClient();
var dict = new Dictionary<string,string> 
{
    { "Name", "Joe Albahari" },
    { "Company", "O'Reilly" }
};
var values = new FormUrlEncodedContent (dict);
var response = await client.PostAsync (uri, values);
response.EnsureSuccessStatusCode();
Console.WriteLine (await response.Content.ReadAsStringAsync());
