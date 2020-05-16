<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{ 
	var dylan = new Person()
	{
		Name = "Dylan",
		Birthdate = new DateTime (1996, 9, 7)
	};
	
	var json1 = JsonSerializer.Serialize (dylan, 
		new JsonSerializerOptions { WriteIndented = true });  
	
	// WriteIndented defaults to false
	var json2 = JsonSerializer.Serialize (dylan); 
             
	json1.Dump();
	json2.Dump();
}

class Person
{
	public string Name { get; set; }
	public DateTime Birthdate { get; set; }
}