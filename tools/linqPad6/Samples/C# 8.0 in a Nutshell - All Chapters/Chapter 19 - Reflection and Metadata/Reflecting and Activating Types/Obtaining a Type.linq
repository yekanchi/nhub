<Query Kind="Program" />

void Main()
{
	Type t1 = DateTime.Now.GetType();        // Type obtained at runtime
	Type t2 = typeof (DateTime);             // Type obtained at compile time
	Type t3 = typeof (DateTime[]);           // 1-d Array type
	Type t4 = typeof (DateTime [,]);         // 2-d Array type
	Type t5 = typeof (Dictionary<int, int>); // Closed generic type
	Type t6 = typeof (Dictionary<,>);        // Unbound generic type

	t1.Dump(1);
	t2.Dump(1);
	t3.Dump(1);
	t4.Dump(1);
	t5.Dump(1);
	t6.Dump(1);

	Type t = Assembly.GetExecutingAssembly().GetType ("Foo.Bar");
	t.Dump(1);
}

namespace Foo
{
	public class Bar
	{
		public int Baz;
	}
}