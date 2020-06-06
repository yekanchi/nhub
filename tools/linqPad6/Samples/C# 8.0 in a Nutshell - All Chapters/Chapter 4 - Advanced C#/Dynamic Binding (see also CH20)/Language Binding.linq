<Query Kind="Program">
  <Namespace>System.Dynamic</Namespace>
</Query>

// Language binding occurs when a dynamic object does not implement IDynamicMetaObjectProvider:

static dynamic Mean (dynamic x, dynamic y) => (x + y) / 2;

static void Main()
{
	int x = 3, y = 4;
	Console.WriteLine (Mean (x, y));
}