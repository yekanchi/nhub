<Query Kind="VBProgram">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Sub Main
	Dim name As XElement = New XElement("name", GetUsersName())
	Console.WriteLine(name)
End Sub

' Define other methods and classes here
Function GetUsersName() As String
	Return "George"
End Function
