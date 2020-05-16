<Query Kind="Program" />

void Main()
{
	Foo (3);
}

void Foo (object obj)
{
	// C# won’t let you use the == operator, because obj is object.
	// However, we can use ‘is’
	if (obj is 3) Console.WriteLine ("three");
}