<Query Kind="Program" />

static void Main()
{
	using (var tmr = new System.Threading.Timer (TimerTick, null, 1000, 1000))
	{
		GC.Collect();
		System.Threading.Thread.Sleep (10000);    // Wait 10 seconds 
	}
}

static void TimerTick (object notUsed) { Console.WriteLine ("tick"); }