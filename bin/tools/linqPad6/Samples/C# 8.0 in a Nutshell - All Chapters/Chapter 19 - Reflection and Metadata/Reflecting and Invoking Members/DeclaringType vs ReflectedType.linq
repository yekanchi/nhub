<Query Kind="Program" />

class Program
{
	static void Main()
	{
		// MethodInfo is a subclass of MemberInfo; see Figure 19-1.

		MethodInfo test = typeof (Program).GetMethod ("ToString");
		MethodInfo obj = typeof (object).GetMethod ("ToString");

		Console.WriteLine (test.DeclaringType);      // System.Object
		Console.WriteLine (obj.DeclaringType);       // System.Object

		Console.WriteLine (test.ReflectedType);      // Program
		Console.WriteLine (obj.ReflectedType);       // System.Object

		Console.WriteLine (test == obj);             // False

		Console.WriteLine (test.MethodHandle == obj.MethodHandle);    // True

		Console.WriteLine (test.MetadataToken == obj.MetadataToken    // True
                           && test.Module == obj.Module);
	}
}