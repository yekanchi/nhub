<Query Kind="Program">
  <Namespace>Outer.Middle.Inner</Namespace>
</Query>

// Note: to add a 'using' directive in LINQPad, go to query properties or press Ctrl+Shift+M

void Main()
{
	Class1 c;    // Don’t need fully qualified name
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