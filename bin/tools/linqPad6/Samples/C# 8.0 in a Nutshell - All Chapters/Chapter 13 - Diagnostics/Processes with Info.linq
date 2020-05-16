<Query Kind="Statements">
  <Namespace>System.ComponentModel</Namespace>
</Query>

foreach (Process p in Process.GetProcesses()
    .Where(pr => pr.ProcessName.StartsWith("L")) // Optional filter to narrow it down
	)
	using (p)
	{
		Console.WriteLine (p.ProcessName);
		Console.WriteLine ("   PID:      " + p.Id);
		Console.WriteLine ("   Memory:   " + p.WorkingSet64);
		Console.WriteLine ("   Threads:  " + p.Threads.Count);
		EnumerateThreads(p);
	}

void EnumerateThreads (Process p)
{
	try
	{
		foreach (ProcessThread pt in p.Threads)
		{
			Console.WriteLine (pt.Id);
			Console.WriteLine ("   State:    " + pt.ThreadState);
			Console.WriteLine ("   Priority: " + pt.PriorityLevel);
			Console.WriteLine ("   Started:  " + pt.StartTime);
			Console.WriteLine ("   CPU time: " + pt.TotalProcessorTime);
		}
	}
	catch (InvalidOperationException ex) // The process may go away while enumerating its threads
	{
		Console.WriteLine ($"Exception: {ex.Message}");
	}
	catch (Win32Exception ex) // We may not have access
	{
		Console.WriteLine ($"Exception: {ex.Message}");
	}
}