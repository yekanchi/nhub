<Query Kind="Program" />

void Main()
{
	Test (3, 3).Dump();
	Test (4, 4).Dump();
	Test (10, 10).Dump();
}

bool Test (int x, int y) =>
	 x * y is var product && product > 10 && product < 100;