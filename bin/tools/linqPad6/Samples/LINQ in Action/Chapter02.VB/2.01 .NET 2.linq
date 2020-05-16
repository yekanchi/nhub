<Query Kind="VBProgram" />

Sub DisplayProcesses()
	' Build up a list of the running processes
	Dim processes As List(Of String) = New List(Of String)()
	For Each process As Process In System.Diagnostics.Process.GetProcesses()
		processes.Add(process.ProcessName)
	Next
	
	' Print out the list of processes to the console
	processes.Dump()
End Sub

Sub Main()
	DisplayProcesses()
End Sub