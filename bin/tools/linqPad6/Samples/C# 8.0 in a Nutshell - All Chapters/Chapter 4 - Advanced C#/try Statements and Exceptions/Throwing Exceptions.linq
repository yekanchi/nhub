<Query Kind="Program" />

// Exceptions can be thrown either by the runtime or in user code:
	
static void Display (string name)
{
	if (name == null)
		throw new ArgumentNullException (nameof (name));

	Console.WriteLine (name);
}

static void Main()
{
	try
	{
		Display (null);
	}
	catch (ArgumentNullException ex)
	{
		Console.WriteLine ("Caught the exception");
	}
}