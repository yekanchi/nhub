<Query Kind="Program" />

// A delegate type declaration is like an abstract method declaration, prefixed with the delegate keyword:
delegate int Transformer (int x);

static void Main()
{
	// To create a delegate instance, assign a method to a delegate variable:	
	Transformer t = Square;          // Create delegate instance
	int result = t(3);               // Invoke delegate
	Console.WriteLine (result);      // 9
}

static int Square (int x) => x * x;