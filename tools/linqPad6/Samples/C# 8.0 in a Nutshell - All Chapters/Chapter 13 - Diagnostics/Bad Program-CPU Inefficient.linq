<Query Kind="Program">
  <Namespace>System.Diagnostics.Tracing</Namespace>
</Query>

// This program is intentially written to perform poorly. You can run it while experimenting with the diagnostics techniques described in Chapter 13.

void Main()
{
	// The diagnostic tools need our process ID:
	Console.WriteLine ($"Our process ID {Process.GetCurrentProcess().Id}");
	Console.WriteLine("Waiting 10 seconds to allow trace start.");
	Thread.Sleep(10000);
	Console.WriteLine(MaxSubarraySlow(LongRandomArray(7000))); // Input value 7000 runs for 1-2 minutes on modern hardware
}

// This poor implementation of the maximum subarray problem gives O(n^3) performance
// For an O(n) solution, look up Kadane's algorithm.
// This method calculates the largest sum you can get by adding the value at contiguous array indices.
int MaxSubarraySlow (int[] array)
{
	if (array?.Count() == 0) throw new ArgumentException ("Array must have elements.");

	Stopwatch maxTimer = Stopwatch.StartNew();
	int highestSum = int.MinValue;
	for (int i = 0; i < array.Length; i++) // Left bound of subarray
		for (int j = i + 1; j < array.Length; j++) // Right bound of subarray
		{
			int subarraySum = 0;
			for (int n = i; n <= j; n++) subarraySum += array [n];
			highestSum = Math.Max (highestSum, subarraySum);
			if (subarraySum == highestSum) // Max found (could equal prior max)
			{
				MyEventCounterSource.Log.Request (highestSum, maxTimer.ElapsedMilliseconds);
				maxTimer.Restart();
			}
		}
	return highestSum;
}

int [] LongRandomArray (int size)
{
	return Enumerable.Repeat (0, size).Select (i => rnd.Next()).ToArray();
}

Random rnd = new Random();

[EventSource (Name = "My-Subarray-MaxUpdated")]
public sealed class MyEventCounterSource : EventSource
{
	public static MyEventCounterSource Log = new MyEventCounterSource();
	private EventCounter requestCounter;

	private MyEventCounterSource() : base (EventSourceSettings.EtwSelfDescribingEventFormat)
	{
		this.requestCounter = new EventCounter ("Highest sum updated", this);
	}

	public void Request (int currentMax, float elapsedMSec)
	{
		WriteEvent (1, currentMax, elapsedMSec);
		this.requestCounter.WriteMetric (elapsedMSec);
	}
}