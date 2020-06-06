<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
</Query>

SemaphoreSlim _semaphore = new SemaphoreSlim (4);  // 4 downloads at a time

void Main()
{
	Util.AutoScrollResults = true;
	for (int i = 0; i < 50; i++)
	{
		int local = i;
		DownloadWithSemaphoreAsync ("http://someinvaliduri/" + i)
			.ContinueWith (c => ("Finished download " + local).Dump());
	}
}

async Task<byte[]> DownloadWithSemaphoreAsync (string uri)
{
	using (await _semaphore.EnterAsync())
		return await new WebClient().DownloadDataTaskAsync (uri);
}

static class Extensions
{
	public static async Task<IDisposable> EnterAsync (this SemaphoreSlim ss)
	{
		await ss.WaitAsync().ConfigureAwait (false);
		return Disposable.Create (() => ss.Release());
	}
}