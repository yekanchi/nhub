<Query Kind="Statements">
  
</Query>

object[] things = {"Sam",1,DateTime.Today,"Eric"};
Console.WriteLine("Dumping just the \"strings\" using Loop");
foreach (var v in things)
if( v.GetType() == typeof(string))
	v.Dump();
Console.WriteLine("Dumping just the \"strings\" using LINQ");
things.OfType<string>().Dump();
