<Query Kind="Program">
  <Namespace>System.Dynamic</Namespace>
</Query>

// You can also call statically known functions with dynamic arguments.
// Such calls are subject to dynamic overload resolution:

static void Foo (int x)    { Console.WriteLine ("1"); }
static void Foo (string x) { Console.WriteLine ("2"); }

static void Main()
{
	dynamic x = 5;
	dynamic y = "watermelon";

	Foo (x);                // 1
	Foo (y);                // 2
}