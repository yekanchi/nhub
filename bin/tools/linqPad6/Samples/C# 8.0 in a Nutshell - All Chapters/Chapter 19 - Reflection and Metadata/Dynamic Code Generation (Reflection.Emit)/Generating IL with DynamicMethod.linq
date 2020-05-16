<Query Kind="Program">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

void Main()
{
	var dynMeth = new DynamicMethod ("Foo", null, null, typeof (Test));
	ILGenerator gen = dynMeth.GetILGenerator();
	gen.EmitWriteLine ("Hello world");
	gen.Emit (OpCodes.Ret);
	dynMeth.Invoke (null, null);                    // Hello world
}

public class Test
{
}