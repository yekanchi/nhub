<Query Kind="Statements">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

// public static double SquareRoot (double value) => Math.Sqrt (value);

AssemblyName aname = new AssemblyName ("MyEmissions");

AssemblyBuilder assemBuilder = AssemblyBuilder.DefineDynamicAssembly (
	aname, AssemblyBuilderAccess.Run);

ModuleBuilder modBuilder = assemBuilder.DefineDynamicModule ("MainModule");

TypeBuilder tb = modBuilder.DefineType ("Widget", TypeAttributes.Public);

MethodBuilder mb = tb.DefineMethod ("SquareRoot",
	MethodAttributes.Static | MethodAttributes.Public,
	CallingConventions.Standard,
	typeof (double),                     // Return type
	new[] { typeof (double) });        // Parameter types

mb.DefineParameter (1, ParameterAttributes.None, "value");  // Assign name

ILGenerator gen = mb.GetILGenerator();
gen.Emit (OpCodes.Ldarg_0);                                // Load 1st arg
gen.Emit (OpCodes.Call, typeof (Math).GetMethod ("Sqrt"));
gen.Emit (OpCodes.Ret);

Type realType = tb.CreateType();
double x = (double)tb.GetMethod ("SquareRoot").Invoke (null,
                                                new object[] { 10.0 });
Console.WriteLine (x);   // 3.16227766016838

// LINQPad can disassemble methods for you:
tb.GetMethod ("SquareRoot").Disassemble().Dump ("LINQPad disassembly");