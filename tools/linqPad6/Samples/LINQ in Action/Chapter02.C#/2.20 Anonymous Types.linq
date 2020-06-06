<Query Kind="Program" />

static void DisplayProcesses(Func<Process, Boolean> match)
{
  var processes = new List<Object>();
  foreach (var process in Process.GetProcesses())
  {
	  if (match(process))
	  {
	    processes.Add( new { process.Id, Name=process.ProcessName, Memory=process.WorkingSet64 } );
	  }
  }

  processes.Dump();
}

static void Main()
{
  DisplayProcesses(process => process.WorkingSet64 >= 20 * 1024 * 1024);
}