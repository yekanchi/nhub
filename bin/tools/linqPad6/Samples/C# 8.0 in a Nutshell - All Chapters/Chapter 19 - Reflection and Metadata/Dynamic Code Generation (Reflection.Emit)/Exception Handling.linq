<Query Kind="Statements">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

var dynMeth = new DynamicMethod ("Test", null, null, typeof (void));
ILGenerator gen = dynMeth.GetILGenerator();

// try                               { throw new NotSupportedException(); }
// catch (NotSupportedException ex)  { Console.WriteLine (ex.Message);    }
// finally                           { Console.WriteLine ("Finally");     }

MethodInfo getMessageProp = typeof (NotSupportedException)
                            .GetProperty ("Message").GetGetMethod();

MethodInfo writeLineString = typeof (Console).GetMethod ("WriteLine",
                                             new[] { typeof (object) } );
gen.BeginExceptionBlock();
{
	ConstructorInfo ci = typeof (NotSupportedException).GetConstructor (
                                                          new Type[0] );
	gen.Emit (OpCodes.Newobj, ci);
	gen.Emit (OpCodes.Throw);
}
gen.BeginCatchBlock (typeof (NotSupportedException));
{
	gen.Emit (OpCodes.Callvirt, getMessageProp);
	gen.Emit (OpCodes.Call, writeLineString);
}
gen.BeginFinallyBlock();
{
	gen.EmitWriteLine ("Finally");
}
gen.EndExceptionBlock();

gen.Emit (OpCodes.Ret);

dynMeth.Invoke (null, null);        // Hello, world!