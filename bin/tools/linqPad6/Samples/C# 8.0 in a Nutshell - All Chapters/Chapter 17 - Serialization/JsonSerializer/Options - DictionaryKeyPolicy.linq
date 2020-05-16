<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	var dict = new Dictionary<string, string>
	{
		{ "ProgramVersion", "1.2" },
		{ "PackageName", "Nutshell" }
	};
	
	Console.WriteLine (JsonSerializer.Serialize (dict,
		new JsonSerializerOptions()
		{
			WriteIndented = true,
			DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
		}));
}

class Person
{
	public string Name { get; set; }
	public Dictionary<string, string> SportsTeams { get; set; }
}