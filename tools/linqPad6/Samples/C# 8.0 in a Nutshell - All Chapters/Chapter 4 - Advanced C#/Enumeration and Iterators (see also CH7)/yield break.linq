<Query Kind="Program" />

// The yield break statement indicates that the iterator block should exit early,
// without returning more elements:

static void Main()
{
	foreach (string s in Foo (true))
		Console.WriteLine(s);
}

static IEnumerable<string> Foo (bool breakEarly)
{
	yield return "One";
	yield return "Two";
	
	if (breakEarly)
		yield break;
	
	yield return "Three";
}