<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

#load ".\Create sample JSON files.linq"

byte[] data = File.ReadAllBytes ("people.json");
Utf8JsonReader reader = new Utf8JsonReader (data);
while (reader.Read())
{
	switch (reader.TokenType)
	{
		case JsonTokenType.StartObject:
			Console.WriteLine ($"Start of object");
			break;
		case JsonTokenType.EndObject:
			Console.WriteLine ($"End of object");
			break;
		case JsonTokenType.StartArray:
			Console.WriteLine();
			Console.WriteLine ($"Start of array");
			break;
		case JsonTokenType.EndArray:
			Console.WriteLine ($"End of array");
			break;
		case JsonTokenType.PropertyName:
			Console.Write ($"Property: {reader.GetString()}");
			break;
		case JsonTokenType.String:
			Console.WriteLine ($" Value: {reader.GetString()}");
			break;
		case JsonTokenType.Number:
			Console.WriteLine ($" Value: {reader.GetInt32()}");
			break;
		default:
			Console.WriteLine ($"No support for {reader.TokenType}");
			break;
	}
}