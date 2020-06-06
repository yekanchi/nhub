<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

// This creates the sample files used in Chapter 11 for JSON.
// This query is #load-ed by other queries, so you don't need to run it directly.
// The files are cleaned up when you exit LINQPad

var people = @"{
	""FirstName"":""Sara"",
	""LastName"":""Wells"",
	""Age"":35,
	""Friends"":[""Dylan"",""Ian""]
}";

if (!File.Exists ("people.json")) File.WriteAllText ("people.json", people);

var peopleArray = @"[
	{
		""FirstName"":""Sara"",
		""LastName"":""Wells"",
		""Age"":35,
		""Friends"":[""Ian""]
	},
	{
		""FirstName"":""Ian"",
		""LastName"":""Weems"",
		""Age"":42,
		""Friends"":[""Joe"",""Eric"",""Li""]
	},
	{
		""FirstName"":""Dylan"",
		""LastName"":""Lockwood"",
		""Age"":46,
		""Friends"":[""Sara"",""Ian""]
	}
]";

if (!File.Exists ("peopleArray.json")) File.WriteAllText ("peopleArray.json", peopleArray);