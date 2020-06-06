<Query Kind="Program" />

// From C# 7, you can switch on multiple types.

static void Main()
{
	TellMeTheType (12);
	TellMeTheType ("hello");
	TellMeTheType (true);
}

static void TellMeTheType (object x)   // object allows any type.
{
	switch (x)
	{
		case int i:
			Console.WriteLine ("It's an int!");
			Console.WriteLine ($"The square of {i} is {i * i}");
			break;
		case string s:
			Console.WriteLine ("It's a string");
			Console.WriteLine ($"The length of {s} is {s.Length}");
			break;
		default:
			Console.WriteLine ("I don't know what x is");
			break;
	}
}