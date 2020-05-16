<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	var dylan = new Person { Name = "Dylan" };

	var json = JsonSerializer.Serialize (dylan,
		new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		});

	var dylan2 = JsonSerializer.Deserialize<Person> (json,
		new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		});

	dylan.Dump();
	json.Dump();
	dylan2.Dump();
}

class Person
{
	public string Name { get; set; }
}