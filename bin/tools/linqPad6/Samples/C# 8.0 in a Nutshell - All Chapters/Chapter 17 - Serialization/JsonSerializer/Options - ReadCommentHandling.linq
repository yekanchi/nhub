<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	string json = @"
	{ 
		""Name"":""Dylan"" // Comment here    
		/* Another comment here */
	}";
	
	var dylan = JsonSerializer.Deserialize<Person>(json, 
		new JsonSerializerOptions() { 
            WriteIndented = true,
            ReadCommentHandling = JsonCommentHandling.Skip
			});  
                 
	json.Dump();
	dylan.Dump();
}

class Person
{
	public string Name { get; set; }
}