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

Dim searchName = "Wooley"

Dim SQL As String = "Select ID, LastName, FirstName, WebSite, TimeStamp    " & _
	"From dbo.Author " & _
	"Where LastName = '" & searchName & "'"

Dim authors As IEnumerable(Of Author) = context.ExecuteQuery(Of Author)(SQL)
authors.Dump()