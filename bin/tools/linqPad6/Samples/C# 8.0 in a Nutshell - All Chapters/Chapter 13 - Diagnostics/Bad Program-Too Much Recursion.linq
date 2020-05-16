<Query Kind="Program" />

// This program is intentially written to perform poorly. You can run it while experimenting with the diagnostics techniques described in Chapter 13.

void Main()
{
	// The diagnostic tools need our process ID:
	Console.WriteLine($"Our process ID {Process.GetCurrentProcess().Id}");
	Console.WriteLine(FibonacciSlow(50)); // Input value of 50 runs for 1-2 minutes on modern hardware
}

// This poor implementation gives a large call stack and O(2^n) performance.
// Far better solutions are known.
ulong FibonacciSlow(ulong fibOf)
{
	if (fibOf <= 1) return 1;
	
	return FibonacciSlow(fibOf - 1) + FibonacciSlow(fibOf - 2);
}