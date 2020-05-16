<Query Kind="Program" />

// From C# 7, you can declare variables on the fly when calling methods with out parameters.

static void Main()
{
	Split ("Stevie Ray Vaughan", out string a, out string b);
	Console.WriteLine (a);                      // Stevie Ray
	Console.WriteLine (b);                      // Vaughan
	
	Split ("Stevie Ray Vaughan", out string x, out _);   // Discard the 2nd param
	Console.WriteLine (x);
}

static void Split (string name, out string firstNames, out string lastName)
{
	int i = name.LastIndexOf (' ');
	firstNames = name.Substring (0, i);
	lastName   = name.Substring (i + 1);
}