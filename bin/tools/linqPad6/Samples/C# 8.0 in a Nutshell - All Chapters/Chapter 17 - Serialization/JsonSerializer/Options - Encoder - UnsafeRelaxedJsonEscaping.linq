<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
</Query>

void Main()
{
	var dylan = "<b>Dylan & Friends</b>";
	
	JsonSerializer.Serialize (dylan).Dump();

	JsonSerializer.Serialize (dylan,
		new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
		}).Dump();
}

class Person
{
	public string Name { get; set; }
	public Dictionary<string, string> SportsTeams { get; set; }
}