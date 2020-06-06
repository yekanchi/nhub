<Query Kind="Program" />

// Anonymous methods are a C# 2.0 feature that has been subsumed largely by C# 3.0 lambda expressions:

delegate int Transformer (int i);

static void Main()
{
	// This can be done more easily with a lambda expression:
	Transformer sqr = delegate (int x) { return x * x; };
	Console.WriteLine (sqr(3));                            // 9
}

// A unique feature of anonymous methods is that you can omit the parameter declaration entirely - even 
// if the delegate expects them. This can be useful in declaring events with a default empty handler:
public static event EventHandler Clicked = delegate { };
// because it avoids the need for a null check before firing the event.

// The following is also legal:
static void HookUp()
{
	Clicked += delegate { Console.WriteLine ("clicked"); };   // No parameters
}