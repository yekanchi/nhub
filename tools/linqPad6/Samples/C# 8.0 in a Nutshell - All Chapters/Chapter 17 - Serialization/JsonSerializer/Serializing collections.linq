<Query Kind="Program">
  <Namespace>System.Text.Json</Namespace>
</Query>

void Main()
{
	var sara = new Person() { Name = "Sara" };
	var ian = new Person() { Name = "Ian" };

	string json = JsonSerializer.Serialize (new[] { sara, ian },
		new JsonSerializerOptions { WriteIndented = true }).Dump ("Json");
		
	Person[] people = JsonSerializer.Deserialize<Person[]>(json);
	people.Dump();
}

public class Person
{
	public string Name { get; set; }
}