<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static AsyncLocal<string> _asyncLocalTest = new AsyncLocal<string>();

void Main()
{
	_asyncLocalTest.Value = "test";
	new Thread (AnotherMethod).Start();
}

void AnotherMethod() => Console.WriteLine (_asyncLocalTest.Value);  // test