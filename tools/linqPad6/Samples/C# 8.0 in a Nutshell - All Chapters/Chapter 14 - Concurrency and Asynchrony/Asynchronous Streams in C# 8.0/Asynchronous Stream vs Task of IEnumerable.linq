<Query Kind="Statements">
  <Namespace>System.Net.Sockets</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

Console.WriteLine($"Starting async Task<IEnumerable<int>>. Data arrives in one group.");

foreach (var data in await RangeTaskAsync(0, 10, 500))
	Console.WriteLine (data);

Console.WriteLine($"Starting async Task<IEnumerable<int>>. Data arrives as available.");

await foreach (var number in RangeAsync (0, 10, 500))
	Console.WriteLine (number);

static async Task<IEnumerable<int>> RangeTaskAsync(int start, int count, int delay)
{
	List<int> data = new List<int>();
	for (int i = start; i < start + count; i++)
	{
		await Task.Delay (delay);
		data.Add (i);
	}

	return data;
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