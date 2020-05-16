<Query Kind="VBStatements" />

' Section 3.4.1

' Method syntax (dot-notation)
Dim processes1 = _
  Process.GetProcesses() _
	.Where(Function(Process) Process.WorkingSet64 > 20 * 1024 * 1024) _
	.OrderByDescending(Function(Process) Process.WorkingSet64) _
	.Select(Function(Process) New With {Process.Id, .Name = Process.ProcessName})

processes1.Dump("With method syntax")

Console.WriteLine()

' Query expression
Dim processes2 = _
  From process In process.GetProcesses() _
  Where process.WorkingSet64 > 20 * 1024 * 1024 _
  Order By process.WorkingSet64 Descending _
  Select New With {process.Id, .Name = process.ProcessName}

processes2.Dump("With query expression")