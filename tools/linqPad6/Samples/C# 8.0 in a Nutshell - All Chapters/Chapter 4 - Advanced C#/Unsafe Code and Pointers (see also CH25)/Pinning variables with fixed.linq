<Query Kind="Program" />

// Value types declared inline within reference types require the reference type to be pinned:

class Test
{
	public int X;
}

static void Main()
{
	Test test = new Test();
	unsafe
	{
		 fixed (int* p = &test.X)   // Pins test
		 {
		 *p = 9;
		 }
		 Console.WriteLine (test.X);
	}	
}