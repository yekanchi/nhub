<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Threading.Channels</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

// The consumer is half as fast as the producer. We compensate by starting two consumers.

Channel<string> channel =
	Channel.CreateBounded<string> (new BoundedChannelOptions (1000)
	{
		// Specifying SingleReader and/or SingleWriter 
		// allows the Channel to make optimizing assumptions
		SingleReader = false,
		SingleWriter = true,
	});
	
var producer = Produce().ContinueWith (_ => channel.Writer.Complete());
var consumer1 = Consume(1);
var consumer2 = Consume(2);

await Task.WhenAll(consumer1, consumer2);

async Task Produce()
{
	for (int i = 0; i < 10; i++)
	{
		await channel.Writer.WriteAsync ($"Msg {i}");
		await Task.Delay (1000);
	}
	Console.WriteLine ("Producer done.");
}

async Task Consume(int id) // We add an ID just to visualize which one processed a given message
{
	while (await channel.Reader.WaitToReadAsync())
	{
		if (channel.Reader.TryRead (out string data))
		{
			Console.WriteLine ($"Processed on {id}: {data}");
			// Simulate processing takes twice as long as producing
			await Task.Delay (2000);
		}
	}
	Console.WriteLine ($"Consumer {id} done.");
}