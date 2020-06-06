<Query Kind="Program">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

void Main()
{
	var dynMeth = new DynamicMethod ("Foo", null, null, typeof (Test));
	ILGenerator gen = dynMeth.GetILGenerator();

	MethodInfo privateMethod = typeof (Test).GetMethod ("HelloWorld",
		BindingFlags.Static | BindingFlags.NonPublic);

	gen.Emit (OpCodes.Call, privateMethod);     // Call HelloWorld
	gen.Emit (OpCodes.Ret);

	dynMeth.Invoke (null, null);                // Hello world
}


public class Test
{
	static void HelloWorld()       // private method, yet we can call it
	{
		Console.WriteLine ("Hello world");
	}
}