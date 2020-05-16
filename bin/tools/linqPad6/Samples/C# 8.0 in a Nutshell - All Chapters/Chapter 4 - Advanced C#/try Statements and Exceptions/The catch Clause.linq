<Query Kind="Program" />

// You can handle multiple exception types with multiple catch clauses:

static void Main() { Main ("one"); }

static void Main (params string[] args)
{
	try
	{
		byte b = byte.Parse (args[0]);
		Console.WriteLine (b);
	}
	catch (IndexOutOfRangeException ex)
	{
		Console.WriteLine ("Please provide at least one argument");
	}
	catch (FormatException ex)
	{
		Console.WriteLine ("That's not a number!");
	}
	catch (OverflowException ex)
	{
		Console.WriteLine ("You've given me more than a byte!");
	}
}

static int Calc (int x) { return 10 / x; }