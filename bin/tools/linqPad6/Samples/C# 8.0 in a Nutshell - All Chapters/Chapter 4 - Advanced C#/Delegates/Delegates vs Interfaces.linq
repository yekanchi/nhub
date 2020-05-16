<Query Kind="Program" />

// A problem that can be solved with a delegate can also be solved with an interface:

public interface ITransformer
{
	int Transform (int x);
}

public class Util
{
	public static void TransformAll (int[] values, ITransformer t)
	{
		for (int i = 0; i < values.Length; i++)
			values[i] = t.Transform (values[i]);
	}
}

class Squarer : ITransformer
{
	public int Transform (int x) => x * x;
}

public static void Main()
{
	int[] values = { 1, 2, 3 };
	Util.TransformAll (values, new Squarer());
	values.Dump();
}