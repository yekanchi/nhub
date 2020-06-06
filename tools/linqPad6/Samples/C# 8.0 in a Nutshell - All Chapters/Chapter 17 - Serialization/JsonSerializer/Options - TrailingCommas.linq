<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Diagnostics</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
  <Namespace>System.Text.Json</Namespace>
</Query>

void Main()
{
	var dylan = new Person_v3()
	{
		Name = "Dylan",
		LuckyNumbers = new List<int>() { 10, 7 },
		Age = 46
	};
	
	JsonSerializerOptions opts = new JsonSerializerOptions();
	opts.WriteIndented = true;
	var json = JsonSerializer.Serialize<Person_v3>(dylan, opts);
	
	"Correct JSON".Dump();
	json.Dump();
	
	var brokenJson = json.Replace("7", "7,").Replace("46", "46,");
	"Broken JSON".Dump(); // Because of trailing commas we just added
	brokenJson.Dump();
	
	"Try to deserialize trailing commas without setting options.".Dump();
	
	try
	{
		var dylanBroken = JsonSerializer.Deserialize<Person_v3>(brokenJson);
	}
	catch (JsonException ex)
	{
		$"As expected, the JSON can't be parsed: {ex.Message}".Dump();
	}
	
	"Deserialize with option AllowTrailingCommas = true".Dump();
	
	var dylanCommaTolerant = JsonSerializer.Deserialize<Person_v3>(brokenJson, 
		new JsonSerializerOptions() { AllowTrailingCommas = true });
	dylanCommaTolerant.Dump();
}

class Person_v3
{
	public string Name { get; set; }
	public List<int> LuckyNumbers { get; set; }
	public int Age { get; set; }
}