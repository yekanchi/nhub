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

//6.7: Project into an anonymous type
//LINQPad abstracts the context. We'll set it to this.
DataContext dataContext = this;

var query =
  	from book in dataContext.GetTable<Book>()
  	select new 
	{
		book.Title,
		book.Price
	};
  
query.Dump();