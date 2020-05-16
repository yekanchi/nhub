<Query Kind="Program" />

// The using statement provides an elegant syntax for calling Dispose on
// an IDisposable object within a finally block:

static void ReadFile()
{
	using (StreamReader reader = File.OpenText ("file.txt"))
	{
		if (reader.EndOfStream) return;
		Console.WriteLine (reader.ReadToEnd());
	}
}

static void Main()
{
	File.WriteAllText ("file.txt", "test");
	ReadFile ();
}