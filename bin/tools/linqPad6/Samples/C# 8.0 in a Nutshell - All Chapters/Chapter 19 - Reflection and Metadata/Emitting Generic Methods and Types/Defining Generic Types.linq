<Query Kind="Statements">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

// public class Widget<T>
// {
//   public T Value;
// }

AssemblyName aname = new AssemblyName ("MyEmissions");

AssemblyBuilder assemBuilder = AssemblyBuilder.DefineDynamicAssembly (
	aname, AssemblyBuilderAccess.Run);

ModuleBuilder modBuilder = assemBuilder.DefineDynamicModule ("MainModule");

TypeBuilder tb = modBuilder.DefineType ("Widget", TypeAttributes.Public);

GenericTypeParameterBuilder[] genericParams = tb.DefineGenericParameters ("T");

tb.DefineField ("Value", genericParams [0], FieldAttributes.Public);