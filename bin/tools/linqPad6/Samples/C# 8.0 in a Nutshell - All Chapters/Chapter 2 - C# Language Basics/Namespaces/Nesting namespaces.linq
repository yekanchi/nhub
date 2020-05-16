<Query Kind="Program" />

void Main()
{
	typeof (Outer.Middle.Inner.Class1).FullName.Dump();
}

namespace Outer
{
	namespace Middle
	{
		namespace Inner
		{
			class Class1 {}
			class Class2 {}
		}
	}
}