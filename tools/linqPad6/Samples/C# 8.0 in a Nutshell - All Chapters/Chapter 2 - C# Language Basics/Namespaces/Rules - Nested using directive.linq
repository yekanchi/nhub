<Query Kind="Program" />

namespace N1
{
	class Class1 {}
}

namespace N2
{
	using N1;

	class Class2 : Class1 {}
}

namespace N2
{
	class Class3 : Class1 { }   // Compile-time error
}