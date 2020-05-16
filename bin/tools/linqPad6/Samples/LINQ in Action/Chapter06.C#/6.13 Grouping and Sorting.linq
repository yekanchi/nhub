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

//6.13: Grouping and Sorting with LINQ to SQL
//LINQPad abstracts the context. We'll set it to this.
DataContext dataContext = this;

var query =
  	from book in dataContext.GetTable<Book>()
	group book by book.Subject into groupedBooks
	orderby groupedBooks.Key
  	select new
	{
		SubjectId = groupedBooks.Key,
		Books = groupedBooks
	};
  
foreach (var groupedBook in query)
{
	Console.WriteLine("Subject: {0}", groupedBook.SubjectId);
	foreach (Book item in groupedBook.Books)
	{
		Console.WriteLine("Book: {0}", item.Title);
	}
}
