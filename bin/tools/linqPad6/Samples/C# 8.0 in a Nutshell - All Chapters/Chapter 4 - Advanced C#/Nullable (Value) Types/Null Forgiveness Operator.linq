<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

#nullable enable

string? foo = SomeMethodReturningNonnullString(); 
Console.WriteLine(foo!.Length);

string? SomeMethodReturningNonnullString()
{
	return "Bar";
}