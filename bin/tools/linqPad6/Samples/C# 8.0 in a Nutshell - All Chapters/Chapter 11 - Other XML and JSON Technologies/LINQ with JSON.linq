<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

#load ".\Create sample JSON files.linq"

var jsonPath = "peopleArray.json";

using var json = File.OpenRead (jsonPath);
using JsonDocument document = JsonDocument.Parse (json);

var query =
	from person in document.RootElement.EnumerateArray()
	select new
	{
		FirstName = person.GetProperty ("FirstName").GetString(),
		Age = person.GetProperty ("Age").GetInt32(),
		Friends =
			from friend in person.GetProperty ("Friends").EnumerateArray()
			select friend.GetString()
	};

query.Dump();