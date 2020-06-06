<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

static void Main()
{
	using var server = new SimpleHttpServer();

	WebClient wc = new WebClient();          // Make a client request.

	Console.WriteLine (wc.DownloadString
		("http://localhost:51111/MyApp/Request.txt"));
}

class SimpleHttpServer : IDisposable
{
	readonly HttpListener listener = new HttpListener();
	
	public SimpleHttpServer() => ListenAsync();  
	async void ListenAsync()
	{
		listener.Prefixes.Add ("http://localhost:51111/MyApp/");  // Listen on
		listener.Start();                                         // port 51111.

		// Await a client request:
		HttpListenerContext context = await listener.GetContextAsync();

		// Respond to the request:
		string msg = "You asked for: " + context.Request.RawUrl;
		context.Response.ContentLength64 = Encoding.UTF8.GetByteCount (msg);
		context.Response.StatusCode = (int)HttpStatusCode.OK;

		using (Stream s = context.Response.OutputStream)
		using (StreamWriter writer = new StreamWriter (s))
			await writer.WriteAsync (msg);
	}

	public void Dispose() => listener.Close();
}