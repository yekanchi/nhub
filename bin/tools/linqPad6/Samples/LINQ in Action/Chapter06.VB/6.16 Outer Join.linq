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

Dim subjects = dataContext.GetTable(Of Subject)()
Dim books = dataContext.GetTable(Of Book)()
Dim query = From subject in subjects _
			Group Join book In books _
			On subject.ID Equals book.Subject Into joinedBooks = Group _
			From joinedBook In joinedBooks.DefaultIfEmpty() _
			Select subject.Name, joinedBook.Title, joinedBook.Price
	
query.Dump()
