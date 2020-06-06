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

//6.19: Original rewritten
//LINQPad abstracts the context. We'll set it to this.
DataContext dataContext = this;

Table<Subject> subjects = dataContext.GetTable<Subject>();
Table<Book> books = dataContext.GetTable<Book>();

var query =
	from subject in subjects
  	join book in books
		on subject.ID equals book.Subject
	where book.Price < 30
	orderby subject.Name
  	select new
	{
		subject.Name,
		book.Title,
		book.Price
	};
  
query.Dump();