<Query Kind="Program" />

void Main()
{
	SomeBigStruct x = default;
	
	Foo (x);      // Calls the first overload
	Foo (in x);   // Calls the second overload

	Bar (x);      // OK (calls the 'in' overload)
	Bar (in x);   // OK (calls the 'in' overload)
}

void Foo (SomeBigStruct a)    => "Foo".Dump();
void Foo (in SomeBigStruct a) => "in Foo".Dump();

void Bar (in SomeBigStruct a) => "in Bar".Dump();

struct SomeBigStruct
{
	public decimal A, B, C, D, E, F, G;
}