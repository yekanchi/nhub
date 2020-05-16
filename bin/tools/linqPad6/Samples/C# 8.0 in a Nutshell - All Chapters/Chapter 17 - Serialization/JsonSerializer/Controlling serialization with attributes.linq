<Query Kind="Program">
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	var p = new Person { Name = "Ian" };
	string json = JsonSerializer.Serialize (p,
                    new JsonSerializerOptions() { WriteIndented = true });
	json.Dump();
	
	Person p2 = JsonSerializer.Deserialize<Person> (json);
	p2.Dump();
}

public class Person
{
	[JsonPropertyName("FullName")]
	public string Name { get; set; }

	[JsonIgnore]
	public decimal NetWorth { get; set; }   // Not serialized
}