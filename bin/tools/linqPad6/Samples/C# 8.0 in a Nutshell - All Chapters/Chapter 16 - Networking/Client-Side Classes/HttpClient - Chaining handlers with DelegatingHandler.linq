<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

async Task Main()
{
	var mocker = new MockHandler (request =>
		new HttpResponseMessage (HttpStatusCode.OK)
		{
			Content = new StringContent ("You asked for " + request.RequestUri)
		});

	var logger = new LoggingHandler (mocker);

	var client = new HttpClient (logger);
	var response = await client.GetAsync ("http://www.linqpad.net");
	string result = await response.Content.ReadAsStringAsync();
	
	Assert.AreEqual ("You asked for http://www.linqpad.net/", result);
}

class MockHandler : HttpMessageHandler
{
	Func<HttpRequestMessage, HttpResponseMessage> _responseGenerator;

	public MockHandler
		(Func<HttpRequestMessage, HttpResponseMessage> responseGenerator)
	{
		_responseGenerator = responseGenerator;
	}

	protected override Task<HttpResponseMessage> SendAsync
		(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		var response = _responseGenerator (request);
		response.RequestMessage = request;
		return Task.FromResult (response);
	}
}

class LoggingHandler : DelegatingHandler
{
	public LoggingHandler (HttpMessageHandler nextHandler)
	{
		InnerHandler = nextHandler;
	}

	protected async override Task<HttpResponseMessage> SendAsync
		(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		Console.WriteLine ("Requesting: " + request.RequestUri);
		var response = await base.SendAsync (request, cancellationToken);
		Console.WriteLine ("Got response: " + response.StatusCode);
		return response;
	}
}

static class Assert
{
	public static void AreEqual (object o1, object o2)
	{
		if (!Equals (o1, o2)) throw new Exception ("Objects are not equal");    
	}    
}