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

dim query = From _book In dataContext.GetTable(Of Book)() _
			Group By key = _book.Subject Into Group _
			Select id = key, groupedBooks = Group

For Each groupedBook in query
	Console.WriteLine("Subject: {0}", groupedBook.id)
	For Each _Book in groupedBook.groupedBooks
		Console.WriteLine("Book: {0}", _Book.Title)
	Next
Next