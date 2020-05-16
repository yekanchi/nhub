<Query Kind="Program" />

class ProcessData
{
  public Int32 Id;
  public Int64 Memory;
  public String Name;
}

static void DisplayProcesses()
{
  // Build up a list of the running processes
  List<ProcessData> processes = new List<ProcessData>();
  foreach (Process process in Process.GetProcesses())
  {
	  ProcessData data = new ProcessData();
	  data.Id = process.Id;
	  data.Name = process.ProcessName;
	  data.Memory = process.WorkingSet64;
	  processes.Add(data);
  }

  processes.Dump();
}

static void Main()
{
  DisplayProcesses();
}