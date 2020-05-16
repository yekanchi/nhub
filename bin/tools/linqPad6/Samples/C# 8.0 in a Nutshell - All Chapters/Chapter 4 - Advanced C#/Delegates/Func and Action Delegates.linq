<Query Kind="Program" />

// With the Func and Action family of delegates in the System namespace, you can avoid the
// need for creating most custom delegate types:

public class Util
{
	// Define this to accept Func<T,TResult> instead of a custom delegate:
	public static void Transform<T> (T[] values, Func<T,T> transformer)
	{
		for (int i = 0; i < values.Length; i++)
			values[i] = transformer (values[i]);
	}
}

static void Main()
{
	int[] values = { 1, 2, 3 };
	Util.Transform (values, Square);      // Dynamically hook in Square
	values.Dump();	
}

static int Square (int x) => x * x;