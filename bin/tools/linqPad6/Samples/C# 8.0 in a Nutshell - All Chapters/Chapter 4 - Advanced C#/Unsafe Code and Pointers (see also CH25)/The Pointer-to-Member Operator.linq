<Query Kind="Program" />

// In addition to the & and * operators, C# also provides the C++ style -> operator,
// which can be used on structs:

struct Test
{
	public int X;
}

unsafe static void Main()
{
	Test test = new Test();
	Test* p = &test;
	p->X = 9;
	Console.WriteLine (test.X);
}

