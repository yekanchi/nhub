<Query Kind="Program" />

// Multiple yield statements are permitted:

static void Main()
{
	foreach (string s in Foo())
		Console.WriteLine(s);         // Prints "One","Two","Three"
}

static IEnumerable<string> Foo()
{
	yield return "One";
	yield return "Two";
	yield return "Three";
}

