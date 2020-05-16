<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	string json = "{ \"name\":\"Dylan\" }";
	
	var dylan1 = JsonSerializer.Deserialize<Person>(json, 
		new JsonSerializerOptions() { 
            WriteIndented = true 
			});  
			
	var dylan2 = JsonSerializer.Deserialize<Person>(json, 
		new JsonSerializerOptions() { 
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
			});   
			
	dylan1.Dump();
	dylan2.Dump();
}

class Person
{
	public string Name { get; set; }
}