<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Channels</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

// The consumer is half as fast as the producer. The producer will finish first.

Channel<string> channel =
	Channel.CreateBounded<string> (new BoundedChannelOptions (1000)
	{
		// Specifying SingleReader and/or SingleWriter 
		// allows the Channel to make optimizing assumptions
		SingleReader = true,
		SingleWriter = true,
	});
var producer = Produce().ContinueWith (_ => channel.Writer.Complete());
var consumer = Consume();

async Task Produce()
{
	for (int i = 0; i < 10; i++)
	{
		await channel.Writer.WriteAsync ($"Msg {i}");
		await Task.Delay(1000);
	}
	Console.WriteLine("Producer done.");
}

async Task Consume()
{
	while (await channel.Reader.WaitToReadAsync())
	{
		if (channel.Reader.TryRead(out string data))
		{
			Console.WriteLine($"Processed: {data}");
			// Simulate processing takes twice as long as producing
			await Task.Delay(2000);
		}
	}
	Console.WriteLine("Consumer done.");
}