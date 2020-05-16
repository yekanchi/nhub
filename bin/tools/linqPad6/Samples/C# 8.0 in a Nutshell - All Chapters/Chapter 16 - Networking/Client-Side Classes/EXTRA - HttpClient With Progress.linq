<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

async Task Main()
{
	var linqPadProgressBar = new Util.ProgressBar ("Download progress").Dump();
	
	var progress = new Progress<double>();
	
	progress.ProgressChanged += (sender, value) =>
		linqPadProgressBar.Percent = (int) value;

	var cancellationToken = new CancellationTokenSource();

	using var destination = File.OpenWrite ("LINQPad6Setup.exe");
	await DownloadFileAsync ("https://www.linqpad.net/GetFile.aspx?LINQPad6Setup.exe", destination, progress, default);
}

// Based on: https://stackoverflow.com/q/21169573/141172
//           https://stackoverflow.com/q/230128/141172

HttpClient client = new HttpClient();

async Task CopyStreamWithProgressAsync (Stream input, Stream output, long total, IProgress<double> progress, CancellationToken token)
{
	const int IO_BUFFER_SIZE = 8 * 1024; // Optimal size depends on your scenario

	// Expected size of input stream may be known from an HTTP header when reading from HTTP. Other streams may have their
	// own protocol for pre-reporting expected size.

	var canReportProgress = total != -1 && progress != null;
	var totalRead = 0L;
	byte[] buffer = new byte [IO_BUFFER_SIZE];
	int read;
	
	while ((read = await input.ReadAsync (buffer, 0, buffer.Length)) > 0)
	{
		token.ThrowIfCancellationRequested();
		await output.WriteAsync (buffer, 0, read);
		totalRead += read;
		if (canReportProgress)
			progress.Report ((totalRead * 1d) / (total * 1d) * 100);
	}
}
 
 async Task DownloadFileAsync (string url, Stream destination, IProgress<double> progress, CancellationToken token)
{
	var response = await client.GetAsync (url, HttpCompletionOption.ResponseHeadersRead, token);

	if (!response.IsSuccessStatusCode)
		throw new Exception (string.Format ("The request returned with HTTP status code {0}", response.StatusCode));

	var total = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : -1L;

	using var source = await response.Content.ReadAsStreamAsync();
	
	await CopyStreamWithProgressAsync(source, destination, total, progress, token);
}