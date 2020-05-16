<Query Kind="Program" />

void Main()
{
	foreach (MethodInfo mi in typeof (Test).GetMethods())
		Console.Write (mi.Name + "  ");

	PropertyInfo pi = typeof (Console).GetProperty ("Title").Dump ("Property");
	
	MethodInfo getter = pi.GetGetMethod().Dump ("Get Method");   // get_Title
	MethodInfo setter = pi.GetSetMethod().Dump ("Set Method");   // set_Title
	MethodInfo[] both = pi.GetAccessors().Dump ("Accessors");    // Length==2

	PropertyInfo p = getter.DeclaringType.GetProperties()
                     .First (x => x.GetAccessors (true).Contains (getter));
	
	Debug.Assert (p == pi);
}

class Test { public int X { get { return 0; } set { } } }

class Walnut
{
	private bool cracked;
	public void Crack() { cracked = true; }
}