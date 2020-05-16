<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static AsyncLocal<StringBuilder> _asyncLocalTest = new AsyncLocal<StringBuilder>();

void Main()
{
	_asyncLocalTest.Value = new StringBuilder ("test");
	var t = new Thread (AnotherMethod);
	t.Start(); t.Join();
	Console.WriteLine (_asyncLocalTest.Value.ToString());   // test haha!
}

void AnotherMethod() => _asyncLocalTest.Value.Append (" ha-ha!");