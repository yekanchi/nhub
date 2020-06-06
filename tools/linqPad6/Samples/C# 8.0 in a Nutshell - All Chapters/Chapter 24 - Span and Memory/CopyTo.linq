<Query Kind="Statements" />

{
	Span<int> x = new[] { 1, 2, 3, 4 };
	Span<int> y = new int [4];
	x.CopyTo (y);
	y.Dump ("Copy");
}

{
	Span<int> x = new[] { 1, 2, 3, 4 };
	Span<int> y = new[] { 10, 20, 30, 40 };
	x [..2].CopyTo (y [2..]);                 // y is now { 10, 20, 1, 2 }
 
	y.Dump ("Copy - with span");
}