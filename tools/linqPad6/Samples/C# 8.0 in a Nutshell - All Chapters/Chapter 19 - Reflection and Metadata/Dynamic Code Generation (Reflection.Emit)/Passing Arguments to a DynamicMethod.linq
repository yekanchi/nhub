<Query Kind="Statements">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

DynamicMethod dynMeth = new DynamicMethod ("Foo",
	typeof (int),                              // Return type = int
	new[] { typeof (int), typeof (int) },      // Parameter types = int, int
	typeof (void));

ILGenerator gen = dynMeth.GetILGenerator();

gen.Emit (OpCodes.Ldarg_0);      // Push first arg onto eval stack
gen.Emit (OpCodes.Ldarg_1);      // Push second arg onto eval stack
gen.Emit (OpCodes.Add);          // Add them together (result on stack)
gen.Emit (OpCodes.Ret);          // Return with stack having 1 value

int result = (int)dynMeth.Invoke (null, new object[] { 3, 4 });   // 7
result.Dump();

// If you need to invoke the method repeatedly, here's an optimized solution:
var func = (Func<int,int,int>)dynMeth.CreateDelegate (typeof (Func<int,int,int>));
result = func (3, 4);      // 7
result.Dump();