<Query Kind="Program" />

class Program
{
	delegate int IntFunc (int x);

	static int Square (int x) => x * x;        // Static method
	int        Cube   (int x) => x * x * x;    // Instance method

	static void Main()
	{
		Delegate staticD = Delegate.CreateDelegate
			(typeof (IntFunc), typeof (Program), "Square");

		Delegate instanceD = Delegate.CreateDelegate
			(typeof (IntFunc), new Program(), "Cube");

		Console.WriteLine (staticD.DynamicInvoke (3));      // 9
		Console.WriteLine (instanceD.DynamicInvoke (3));    // 27

		IntFunc f = (IntFunc)staticD;
		Console.WriteLine (f (3));         // 9 (but much faster!)
	}
}