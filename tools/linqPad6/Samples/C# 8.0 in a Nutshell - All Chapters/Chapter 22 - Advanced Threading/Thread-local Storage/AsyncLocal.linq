<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static AsyncLocal<string> _asyncLocalTest = new AsyncLocal<string>();

async Task Main()
{
	Thread.CurrentThread.ManagedThreadId.Dump ("Current Thread ID");
	_asyncLocalTest.Value = "test";
	
	await Task.Delay (1000);
	
	Thread.CurrentThread.ManagedThreadId.Dump ("Current Thread ID");
	Console.WriteLine (_asyncLocalTest.Value);
}