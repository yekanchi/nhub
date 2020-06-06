<Query Kind="Program" />

void Main()
{
	Console.WriteLine("Display current values of performance counters:");
	
	using PerformanceCounter pc1 = new PerformanceCounter ("Processor",
                                                          "% Processor Time",
                                                          "_Total");
	Console.WriteLine (pc1.NextValue());
	
	string procName = Process.GetCurrentProcess().ProcessName;
	using PerformanceCounter pc2 = new PerformanceCounter ("Process",
                                                          "Private Bytes",
                                                          procName);
	Console.WriteLine (pc2.NextValue());
	
	Console.WriteLine("Monitor performance counters:");

	EventWaitHandle stopper = new ManualResetEvent (false);

	new Thread (() =>
		Monitor ("Processor", "% Processor Time", "_Total", stopper)
	).Start();

	new Thread (() =>
		Monitor ("LogicalDisk", "% Idle Time", "C:", stopper)
	).Start();

	
	// When running in LINQPad, we'll monitor for 60 seconds then exit. Stop the query to end early.
	Console.WriteLine ("Monitoring - wait 60 seconds or stop query to quit");
	Thread.Sleep(60 * 1000);
	
	// In a console app, you can run until a key is pressed:
	//Console.WriteLine ("Monitoring - press any key to quit");
	//Console.ReadKey();
	
	stopper.Set();
}

void Monitor (string category, string counter, string instance,
                     EventWaitHandle stopper)
{
	if (!PerformanceCounterCategory.Exists (category))
		throw new InvalidOperationException ("Category does not exist");

	if (!PerformanceCounterCategory.CounterExists (counter, category))
		throw new InvalidOperationException ("Counter does not exist");

	if (instance == null) instance = "";   // "" == no instance (not null!)
	if (instance != "" &&
        !PerformanceCounterCategory.InstanceExists (instance, category))
		throw new InvalidOperationException ("Instance does not exist");

	float lastValue = 0f;
	using (PerformanceCounter pc = new PerformanceCounter (category,
                                                        counter, instance))
		while (!stopper.WaitOne (200, false))
		{
			float value = pc.NextValue();
			if (value != lastValue)         // Only write out the value
			{                               // if it has changed.
				Console.WriteLine (value);
				lastValue = value;
			}
		}
}