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

//6.24: Running query using object heirarchies
//LINQPad abstracts the context. We'll set it to this.
DataContext dataContext = this;

var Subjects = dataContext.GetTable<Subject>();

var query =
  	from subject in Subjects
	orderby subject.Name
	select new 
	{
		subject.Name,
		Books = from book in subject.Books
				where book.Price < 30
				select new 
				{
					book.Title,
					book.Price
				}
	};
	
query.Dump();