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
	
	TypeBuilder[] uncreatedTypes = { aBuilder, bBuilder };

	ResolveEventHandler handler = delegate (object o, ResolveEventArgs args)
	{
		var type = uncreatedTypes.FirstOrDefault (t => t.FullName == args.Name);
		return type == null ? null : type.CreateType().Assembly;
	};

	AppDomain.CurrentDomain.TypeResolve += handler;

	Type realA = aBuilder.CreateType();
	Type realB = bBuilder.CreateType();

	AppDomain.CurrentDomain.TypeResolve -= handler;
}

public struct S<T>
{
	public T SomeField;
}