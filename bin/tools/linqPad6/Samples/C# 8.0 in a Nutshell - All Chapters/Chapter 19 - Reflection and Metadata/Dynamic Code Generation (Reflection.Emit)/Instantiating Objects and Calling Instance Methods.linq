<Query Kind="Statements">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

var dynMeth = new DynamicMethod ("Test", null, null, typeof (void));
ILGenerator gen = dynMeth.GetILGenerator();

ConstructorInfo ci = typeof (StringBuilder).GetConstructor (new Type [0]);
gen.Emit (OpCodes.Newobj, ci);

gen.Emit (OpCodes.Callvirt, typeof (StringBuilder)
                            .GetProperty ("MaxCapacity").GetGetMethod());

gen.Emit (OpCodes.Call, typeof (Console).GetMethod ("WriteLine",
                                         new[] { typeof (int) } ));
gen.Emit (OpCodes.Ret);

dynMeth.Invoke (null, null);              // 2147483647
