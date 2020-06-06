<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

var options = new JsonWriterOptions { Indented = true };

using (var stream = File.Create ("MyFile.json"))
using (var writer = new Utf8JsonWriter (stream, options))
{
	writer.WriteStartObject();
	// Property name and value specified in one call
	writer.WriteString ("FirstName", "Dylan");
	writer.WriteString ("LastName", "Lockwood");
	// Property name and value specified in separate calls
	writer.WritePropertyName ("Age");
	writer.WriteNumberValue (46);
	writer.WriteCommentValue ("This is a (non-standard) comment");
	writer.WriteEndObject();
}

File.ReadAllText("MyFile.json").Dump();