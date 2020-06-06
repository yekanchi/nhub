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

Dim dataContext As DataContext = Me
Console.WriteLine("This example will intentionally fail because we are trying to use unmapped CLR methods")

Dim books = dataContext.GetTable(Of Book)()
Dim query = From book In books _
			Where book.PubDate >= DateTime.Parse("1/1/2007")  _
			Select book.PubDate.Value.ToString("MM/dd/yyyy")
	
query.Dump()
