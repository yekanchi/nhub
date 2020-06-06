<Query Kind="Program" />

// You can, however, yield within a try block that has (only) a finally block:

static void Main()
{
	foreach (string s in Foo()) s.Dump();
	
	Console.WriteLine();
		
	foreach (string s in Foo())
	{
		("First element is " + s).Dump();
		break;
	}
}

static IEnumerable<string> Foo()
{
	try 
	{
		yield return "One";
		yield return "Two";
		yield return "Three";
	}
	finally { "Finally".Dump(); }
}