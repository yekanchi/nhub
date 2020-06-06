<Query Kind="Statements" />

// Section 3.4.1

// Method syntax (dot-notation)
var processes1 =
  Process.GetProcesses()
	  .Where(process => process.WorkingSet64 > 20*1024*1024)
	  .OrderByDescending(process => process.WorkingSet64)
	  .Select(process => new { process.Id, Name=process.ProcessName });

processes1.Dump("With method syntax");

// Query expression
var processes2 = 
  from process in Process.GetProcesses()
  where process.WorkingSet64 > 20*1024*1024
  orderby process.WorkingSet64 descending
  select new { process.Id, Name=process.ProcessName };

processes2.Dump("With query expression");