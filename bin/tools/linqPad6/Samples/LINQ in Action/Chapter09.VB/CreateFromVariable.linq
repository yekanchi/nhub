<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim usersName As String = "Fred"
Dim name As XElement = New XElement("name", usersName)
Console.WriteLine(name)
