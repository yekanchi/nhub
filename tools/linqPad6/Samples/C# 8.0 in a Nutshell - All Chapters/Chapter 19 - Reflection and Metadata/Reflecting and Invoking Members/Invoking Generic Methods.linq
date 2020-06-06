<Query Kind="Program" />

class Program
{
	public static T Echo<T> (T x) { return x; }

	static void Main()
	{
		MethodInfo echo = typeof (Program).GetMethod ("Echo");
		Console.WriteLine (echo.IsGenericMethodDefinition);    // True
		
		try
		{
			echo.Invoke (null, new object[] { 123 });              // Exception
		}
		catch (Exception ex)
		{
			ex.Dump ("This can't be done");
		}

		MethodInfo intEcho = echo.MakeGenericMethod (typeof (int));
		Console.WriteLine (intEcho.IsGenericMethodDefinition);            // False
		Console.WriteLine (intEcho.Invoke (null, new object[] { 3 }));   // 3

	}
}