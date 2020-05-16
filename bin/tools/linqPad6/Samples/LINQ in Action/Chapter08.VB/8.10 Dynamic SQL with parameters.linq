<Query Kind="VBStatements">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim context = Me

Dim searchName = "Good' OR ''='"

Dim SQL As String = "Select ID, LastName, FirstName, WebSite, TimeStamp    " & _
	"From dbo.Author " & _
	"Where LastName = {0}"

Dim authors As IEnumerable(Of Author) = context.ExecuteQuery(Of Author)(SQL, searchName)
Console.WriteLine("No results should be found because injection is thwarted")
authors.Dump()