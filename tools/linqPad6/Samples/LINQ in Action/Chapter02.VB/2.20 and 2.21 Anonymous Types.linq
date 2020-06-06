<Query Kind="VBProgram" />

Sub DisplayProcesses(ByVal match As Func(Of Process, Boolean))
	Dim processes = New List(Of Object)()
	For Each process In System.Diagnostics.Process.GetProcesses()
		If match(process) Then
			processes.Add(New With {Key process.Id, _
				.Name = process.ProcessName, .Memory = process.WorkingSet64})
		End If
	Next

	processes.Dump()
End Sub

Sub TestKeyedAnonymousTypes()
	Dim v1 = New With {Key .Id = 123, .Name = "Fabrice"}
	Dim v2 = New With {Key .Id = 123, .Name = "Céline"}
	Dim v3 = New With {Key .Id = 456, .Name = "Fabrice"}
	v1.Equals(v2).Dump()
	v1.Equals(v3).Dump()
End Sub

Sub Main()
	DisplayProcesses(Function(process) process.WorkingSet64 >= 20 * 1024 * 1024)
	TestKeyedAnonymousTypes()
End Sub