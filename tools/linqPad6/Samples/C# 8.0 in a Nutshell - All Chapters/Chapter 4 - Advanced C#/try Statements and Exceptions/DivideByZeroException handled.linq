<Query Kind="Program" />

// We can catch the DivideByZeroException as follows:

static int Calc (int x) { return 10 / x; }

static void Main()
{
	try
	{
		int y = Calc (0);
		Console.WriteLine (y);
	}
	catch (DivideByZeroException ex)
	{
		Console.WriteLine ("x cannot be zero");
	}
	Console.WriteLine ("program completed");
}