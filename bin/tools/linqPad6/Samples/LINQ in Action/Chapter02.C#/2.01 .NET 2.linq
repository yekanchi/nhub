<Query Kind="Program" />

static void DisplayProcesses()
{
  // Build up a list of the running processes
  List<String> processes = new List<String>();
  foreach (Process process in Process.GetProcesses())
	processes.Add(process.ProcessName);

  processes.Dump();
}

static void Main()
{
  DisplayProcesses();
}