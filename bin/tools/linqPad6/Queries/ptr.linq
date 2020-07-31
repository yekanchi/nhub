<Query Kind="Program" />

void Main()
{
	var x = 662;
	unsafe
	{
		var ptr = &x;
		Console.WriteLine((int)ptr);
	}
	
}

// Define other methods, classes and namespaces here
