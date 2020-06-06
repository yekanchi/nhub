<Query Kind="Statements">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

// public static void SquareRoot (ref double value) => value = Math.Sqrt (value);

AssemblyName aname = new AssemblyName ("MyEmissions");

AssemblyBuilder assemBuilder = AssemblyBuilder.DefineDynamicAssembly (
	aname, AssemblyBuilderAccess.Run);

ModuleBuilder modBuilder = assemBuilder.DefineDynamicModule ("MainModule");

TypeBuilder tb = modBuilder.DefineType ("Widget", TypeAttributes.Public);

MethodBuilder mb = tb.DefineMethod ("SquareRoot",
	MethodAttributes.Static | MethodAttributes.Public,
	CallingConventions.Standard,
	null,
	new Type[] { typeof (double).MakeByRefType() } );

// For an 'out' parameter, use ParameterAttributes.Out instead:
mb.DefineParameter (1, ParameterAttributes.None, "value");

ILGenerator gen = mb.GetILGenerator();
gen.Emit (OpCodes.Ldarg_0);
gen.Emit (OpCodes.Ldarg_0);
gen.Emit (OpCodes.Ldind_R8);
gen.Emit (OpCodes.Call, typeof (Math).GetMethod ("Sqrt"));
gen.Emit (OpCodes.Stind_R8);
gen.Emit (OpCodes.Ret);

Type realType = tb.CreateType();
object[] args = { 10.0 };
tb.GetMethod ("SquareRoot").Invoke (null, args);
Console.WriteLine (args [0]);                     // 3.16227766016838

// LINQPad can disassemble methods for you:
tb.GetMethod ("SquareRoot").Disassemble().Dump ("LINQPad disassembly");