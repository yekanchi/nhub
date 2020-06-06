<Query Kind="Program">
  <Namespace>System.Reflection.Emit</Namespace>
  <UseNoncollectibleLoadContext>true</UseNoncollectibleLoadContext>
</Query>

// class A { S<B> Bee; }
// class B { S<A> Aye; }

void Main()
{ 
	AssemblyName aname = new AssemblyName ("MyEmissions");
	
	AssemblyBuilder assemBuilder = AssemblyBuilder.DefineDynamicAssembly (
		aname, AssemblyBuilderAccess.Run);
	
	ModuleBuilder modBuilder = assemBuilder.DefineDynamicModule ("MainModule");
	
	var pub = FieldAttributes.Public;
	
	TypeBuilder aBuilder = modBuilder.DefineType ("A");
	TypeBuilder bBuilder = modBuilder.DefineType ("B");
	
	aBuilder.DefineField ("Bee", typeof (S<>).MakeGenericType (bBuilder), pub);
	bBuilder.DefineField ("Aye", typeof (S<>).MakeGenericType (aBuilder), pub);
	
	Type realA = aBuilder.CreateType();    // TypeLoadException: cannot load type B
	Type realB = bBuilder.CreateType();
}

public struct S<T>
{
	public T SomeField;
}