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

//6.11a: Using unmapped CLR methods

//LINQPad abstracts the context. We'll set it to this.
DataContext dataContext = this;

var books = dataContext.GetTable<Book>();

// This example will intentionally fail because it uses CLR methods that aren't supported

var query =
  	from book in books
	where book.PubDate > DateTime.Parse("1/1/2007")
  	select book.PubDate.Value.ToString("MM/dd/yyyy");
  
query.Dump();