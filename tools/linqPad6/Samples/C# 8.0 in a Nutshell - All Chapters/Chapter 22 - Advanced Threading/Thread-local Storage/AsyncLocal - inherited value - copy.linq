<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static AsyncLocal<string> _asyncLocalTest = new AsyncLocal<string>();

void Main()
{
	_asyncLocalTest.Value = "test";
	var t = new Thread (AnotherMethod);
	t.Start(); t.Join();
	Console.WriteLine (_asyncLocalTest.Value);   // test  (not ha-ha!)
}

void AnotherMethod() => _asyncLocalTest.Value = "ha-ha!";