<Query Kind="Program" />

// A yield return statement cannot appear in a try block that has a catch clause:

static void Main()
{
	foreach (string s in Foo())
		Console.WriteLine(s);
}

static IEnumerable<string> Foo()
{
	try { yield return "One"; }		// Illegal
	catch { /*...*/ }
}