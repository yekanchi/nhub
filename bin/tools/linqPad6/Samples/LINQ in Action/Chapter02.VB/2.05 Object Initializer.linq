<Query Kind="VBProgram" />

Class ProcessData
	Public Id As Int32
	Public Memory As Int64
	Public Name As String
End Class

Sub DisplayProcesses()
	Dim processes As List(Of ProcessData) = New List(Of ProcessData)()
	For Each process As Process In System.Diagnostics.Process.GetProcesses()
		processes.Add(New ProcessData With {.Id = process.Id, _
			.Name = process.ProcessName, .Memory = process.WorkingSet64})
	Next
	
	' Print out the list of processes to the console
	processes.Dump()
End Sub

Sub Main()
	DisplayProcesses()
End Sub