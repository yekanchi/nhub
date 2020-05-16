<Query Kind="Program" />

ref struct Point { public int X, Y; }

class MyClass { Point P; }         // Error: will not compile!

static void Main()
{
	var points = new Point [100];    // Error: will not compile!
}