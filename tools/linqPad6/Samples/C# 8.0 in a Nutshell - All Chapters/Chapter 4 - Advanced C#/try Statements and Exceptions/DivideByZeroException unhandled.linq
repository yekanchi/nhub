<Query Kind="Program" />

// Because Calc is called with x==0, the runtime throws a DivideByZeroException: 

static int Calc (int x) { return 10 / x; }

static void Main()
{
	int y = Calc (0);
	Console.WriteLine (y);
}
