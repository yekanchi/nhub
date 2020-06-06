<Query Kind="Program">
  <NuGetReference>Microsoft.Diagnostics.Tracing.TraceEvent</NuGetReference>
  <Namespace>Microsoft.Diagnostics.Tracing</Namespace>
  <Namespace>Microsoft.Diagnostics.Tracing.Session</Namespace>
  <Namespace>Microsoft.Diagnostics.Tracing.Parsers.Clr</Namespace>
  <Namespace>Microsoft.Diagnostics.Tracing.Parsers</Namespace>
  <Namespace>System.Runtime</Namespace>
</Query>

void Main()
{
	long totalMemAllocated = 0;
	for (int i = 0; i < 200; i++)
	{
		totalMemAllocated += AllocateSomeNonreferencedMemory();
		string procName = Process.GetCurrentProcess().ProcessName;
		using PerformanceCounter pcPB = new PerformanceCounter ("Process", "Private Bytes", procName);
		long memoryUsed = GC.GetTotalMemory (false); // Change to true to force a collection before reporting used memory
		Console.WriteLine ($"Currently OS allocated: {pcPB.NextValue()}. Current GC reported {memoryUsed}. Allocated at some point {totalMemAllocated}.");
	}
}

long AllocateSomeNonreferencedMemory()
{
	int loops = 64;
	int size = 1024;
	for (int i = 0; i < loops; i++)
	{
		int[] array = new int [size];
	}
	
	return loops * size * 4; // int is 32-bits (4 bytes)
}