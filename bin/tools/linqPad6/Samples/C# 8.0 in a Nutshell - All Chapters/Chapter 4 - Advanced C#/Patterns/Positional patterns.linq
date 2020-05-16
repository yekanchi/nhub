<Query Kind="Program" />

void Main()
{
	Print (new Point (0, 0)).Dump();
}

class Point
{
	public readonly int X, Y;
	
	public Point (int x, int y) => (X, Y) = (x, y);
	
	public void Deconstruct (out int x, out int y)
	{
		x = X; y = Y;
	}
}

string Print (object obj) => obj switch
{
	Point (0, 0) => "Empty point",
	Point (var x, var y) when x == y => "Diagonal",
	_ => "Other"
};