<Query Kind="Program" />

// finally blocks are typically used for cleanup code:

static void ReadFile()
{
	StreamReader reader = null;    // In System.IO namespace
	try
	{
		reader = File.OpenText ("file.txt");
		if (reader.EndOfStream) return;
		Console.WriteLine (reader.ReadToEnd());
	}
	finally
	{
		if (reader != null) reader.Dispose();
	}
}

static void Main()
{
	File.WriteAllText ("file.txt", "test");
	ReadFile ();
}