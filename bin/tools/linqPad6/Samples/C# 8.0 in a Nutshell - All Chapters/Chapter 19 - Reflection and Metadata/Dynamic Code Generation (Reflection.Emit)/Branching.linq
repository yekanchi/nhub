<Query Kind="Statements">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

//int x = 5;
//while (x <= 10) Console.WriteLine (x++);

var dynMeth = new DynamicMethod ("Test", null, null, typeof (void));
ILGenerator gen = dynMeth.GetILGenerator();

Label startLoop = gen.DefineLabel();                  // Declare labels
Label endLoop = gen.DefineLabel();

LocalBuilder x = gen.DeclareLocal (typeof (int));     // int x
gen.Emit (OpCodes.Ldc_I4, 5);                         //
gen.Emit (OpCodes.Stloc, x);                          // x = 5
gen.MarkLabel (startLoop);
{
	gen.Emit (OpCodes.Ldc_I4, 10);              // Load 10 onto eval stack
	gen.Emit (OpCodes.Ldloc, x);                // Load x onto eval stack

	gen.Emit (OpCodes.Blt, endLoop);            // if (x > 10) goto endLoop

	gen.EmitWriteLine (x);                      // Console.WriteLine (x)

	gen.Emit (OpCodes.Ldloc, x);                // Load x onto eval stack
	gen.Emit (OpCodes.Ldc_I4, 1);               // Load 1 onto the stack
	gen.Emit (OpCodes.Add);                     // Add them together
	gen.Emit (OpCodes.Stloc, x);                // Save result back to x

	gen.Emit (OpCodes.Br, startLoop);           // return to start of loop
} 
gen.MarkLabel (endLoop);

gen.Emit (OpCodes.Ret);

dynMeth.Invoke (null, null);