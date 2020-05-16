<Query Kind="Program">
  <Namespace>System.Dynamic</Namespace>
</Query>

// You cannot dynamically call:
//  • Extension methods (via extension method syntax)
//  • Any member of an interface
//  • Base members hidden by a subclass

interface IFoo   { void Test();        }
class Foo : IFoo { void IFoo.Test() {} }

static void Main()
{
	IFoo f = new Foo();
	dynamic d = f;
	d.Test();             // Exception thrown
}
