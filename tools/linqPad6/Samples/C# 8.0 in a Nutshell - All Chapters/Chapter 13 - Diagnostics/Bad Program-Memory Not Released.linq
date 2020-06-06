<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Diagnostics.Tracing</Namespace>
</Query>

// This program is intentially written to perform poorly. You can run it while experimenting with the diagnostics techniques described in Chapter 13.

void Main()
{
	// The diagnostic tools need our process ID:
	Console.WriteLine ($"Our process ID {Process.GetCurrentProcess().Id}");
	MemoryLeak();
}

static Dictionary<string, int> cacheThatNeverCleansUp = new Dictionary<string, int>();

void MemoryLeak()
{
	// Pretend this is an expensive calculation worth caching
	int CalculateSentenceScore (string sentence)
	{
		Stopwatch watch = Stopwatch.StartNew();
		try
		{
			if (cacheThatNeverCleansUp.TryGetValue (sentence, out int score))
				return score;

			var calculatedScore = sentence.Split (' ').Count();
			cacheThatNeverCleansUp.Add (sentence, calculatedScore);
			return calculatedScore;
		}
		finally
		{
			MyEventCounterSource.Log.Request(sentence, (float)watch.ElapsedMilliseconds);
		}
	}

	while (true) // Simulate e.g. a web service that keeps taking requests
	{
		var input = RandomSentence();
		var score = CalculateSentenceScore (input);
		// A web service might return the score to a caller    
	}
}

string RandomSentence()
{
	const string alpha = "abcdefghijklmnopqrstuvwxyz";
	List<string> words = new List<string>();
	int numWords = rnd.Next (2, 15);
	for (int w = 0; w < numWords; w++)
	{
		int wordLen = rnd.Next (1, 10);
		words.Add (new string (Enumerable.Repeat (alpha, wordLen)
			.Select (_ => _ [rnd.Next (_.Length)]).ToArray()));
	}
	return string.Join (' ', words);
}

int [] LongRandomArray (int size)
{
	return Enumerable.Repeat (0, size).Select (i => rnd.Next()).ToArray();
}

Random rnd = new Random();

[EventSource(Name = "My-EventCounter-Minimal")]
public sealed class MyEventCounterSource : EventSource
{
	public static MyEventCounterSource Log = new MyEventCounterSource();
	private EventCounter requestCounter;

	private MyEventCounterSource() : base (EventSourceSettings.EtwSelfDescribingEventFormat)
	{
		this.requestCounter = new EventCounter ("Sentence Request", this);
	}

	public void Request (string sentence, float elapsedMSec)
	{
		WriteEvent (1, sentence, elapsedMSec);
		this.requestCounter.WriteMetric (elapsedMSec);
	}
}