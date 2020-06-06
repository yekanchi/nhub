<Query Kind="Program" />

// A delegate can have more specific parameter types than its method target. This is called contravariance:

delegate object ObjectRetriever();

static void Main()
{
	ObjectRetriever o = new ObjectRetriever (RetriveString);
	object result = o();
	Console.WriteLine (result);      // hello
}

static string RetriveString() => "hello";