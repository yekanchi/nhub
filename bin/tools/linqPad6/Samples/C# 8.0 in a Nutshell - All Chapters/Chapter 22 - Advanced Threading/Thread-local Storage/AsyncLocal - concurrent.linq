<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static AsyncLocal<string> _asyncLocalTest = new AsyncLocal<string>();

void Main()
{
	new Thread (() => Test ("one")).Start();
	new Thread (() => Test ("two")).Start();
}

async void Test (string value)
{
	_asyncLocalTest.Value = value;
	await Task.Delay (1000);
	Console.WriteLine (value + " " + _asyncLocalTest.Value);
}