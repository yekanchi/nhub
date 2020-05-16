<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	var dylan = new Person { Name = null };

	JsonSerializer.Serialize (dylan, 
		new JsonSerializerOptions { WriteIndented = true }
		).Dump("default");

	JsonSerializer.Serialize (dylan,
		new JsonSerializerOptions
		{ 
			WriteIndented = true,
			IgnoreNullValues = true
		}).Dump ("with IgnoreNullValues");
}

class Person
{
	public string Name { get; set; }
	public int Age 
	{ 
		get
		{
			var age = DateTime.Today.Year - Birthdate.Year;
			if (Birthdate.Date > DateTime.Today.AddYears(-age)) age--;
			return age;
		}
	}
	public DateTime Birthdate { get; set; }
}