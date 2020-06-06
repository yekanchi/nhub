<Query Kind="Program" />

delegate int Transformer (int x);

static void Main()
{
	Transformer t = new Transformer (Square);   // Create delegate instance
	int result = t.Invoke (3);                  // Invoke delegate
	Console.WriteLine (result);                 // 9
}

static int Square (int x) => x * x;