<Query Kind="Program">
  <NuGetReference>System.Linq.Async</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	IAsyncEnumerable<int> query =
		from i in RangeAsync (0, 10, 500)
		where i % 2 == 0   // Even numbers only.
		select i * 10;     // Multiply by 10.

	await foreach (var number in query)
		Console.WriteLine (number);
		
	query.Dump();   // in LINQPad, you can directly dump IAsyncEnumerable<T>
}

async IAsyncEnumerable<int> RangeAsync (
	int start, int count, int delay)
{
	for (int i = start; i < start + count; i++)
	{
		await Task.Delay (delay);
		yield return i;
	}
}