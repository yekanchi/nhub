<Query Kind="Program" />

void Main()
{
	// Fetch the constructor that accepts a single parameter of type string:
	ConstructorInfo ci = typeof (X).GetConstructor (new[] { typeof (string) });
	
	// Construct the object using that overload, passing in null:
	object foo = ci.Invoke (new object[] { null });  
	foo.Dump();
}

class X
{
	public X (string s)
	{
	}
	
	public X (StringBuilder sb)
	{    
	}
}