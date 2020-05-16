<Query Kind="Program" />

namespace N
{
	class A
	{
		static void Main()
		{
			new A.B().Dump();           // Instantiate nested class B
			new global::A.B().Dump();   // Instantiate class B in namespace A
		}

		public class B { }            // Nested type
	}
}

namespace A
{
	class B { }
}