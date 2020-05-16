<Query Kind="Statements">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
</Query>

//8.10 dynamic SQL pass-through with parameters

string searchName = "Good' OR ''='";
TypedDataContext context = this;

string sql = @"Select ID, LastName, FirstName, WebSite, TimeStamp    " +
		  "From dbo.Author " +
		  "Where LastName = {0}";

IEnumerable<Author> authors = context.ExecuteQuery<Author>(sql, searchName);
authors.Dump();
Console.WriteLine("No records should be fetched with this query");