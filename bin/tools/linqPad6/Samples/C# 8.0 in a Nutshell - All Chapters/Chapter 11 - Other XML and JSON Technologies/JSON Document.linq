<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

Number();
Array();
Age();

void Number()
{
	using JsonDocument document = JsonDocument.Parse ("123");
	JsonElement root = document.RootElement;
	Console.WriteLine (root.ValueKind);       // Number

	int number = document.RootElement.GetInt32();
	Console.WriteLine (number);                // 123
}

void Array()
{
	using JsonDocument document = JsonDocument.Parse (@"[1, 2, 3, 4, 5]");
	int length = document.RootElement.GetArrayLength();   // 5
	int value = document.RootElement [3].GetInt32();      // 4
	
	Console.WriteLine($"length: {length}; value {value}");
}

void Age()
{
	using JsonDocument document = JsonDocument.Parse (@"{ ""Age"": 32}");
	JsonElement root = document.RootElement;
	int age = root.GetProperty ("Age").GetInt32();
	Console.WriteLine(age);

	// Discover Age property
	JsonProperty ageProp = root.EnumerateObject().First();
	string name = ageProp.Name;             // Age  
	JsonElement value = ageProp.Value;
	Console.WriteLine (value.ValueKind);    // Number
	Console.WriteLine (value.GetInt32());   // 32

}