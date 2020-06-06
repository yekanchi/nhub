<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

#load ".\Create sample JSON files.linq"

using JsonDocument document = JsonDocument.Parse (peopleArray);

var options = new JsonWriterOptions { Indented = true };

using (var stream = File.Create ("MyFile.json"))
using (var writer = new Utf8JsonWriter (stream, options))
{
	writer.WriteStartArray();
	foreach (var person in document.RootElement.EnumerateArray())
	{
		int friendCount = person.GetProperty ("Friends").GetArrayLength();
		if (friendCount >= 2)
			person.WriteTo (writer);
	}
}

File.ReadAllText ("MyFile.json").Dump();