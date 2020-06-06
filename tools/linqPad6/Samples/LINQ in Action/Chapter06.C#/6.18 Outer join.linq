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

//6.18: Approximating an Outer Join
//LINQPad abstracts the context. We'll set it to this.
DataContext dataContext = this;

var books = dataContext.GetTable<Book>();
var Subjects = dataContext.GetTable<Subject>();

var query =
	from subject in Subjects
  	join book in books
		on subject.ID equals book.Subject into joinedBooks
	from joinedBook in joinedBooks.DefaultIfEmpty()
  	select new
	{
		subject.Name,
		joinedBook.Title,
		joinedBook.Price
	};
  
query.Dump();