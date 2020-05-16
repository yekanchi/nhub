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

//6.5: Fetch books using LINQ to SQL
//LINQPad abstracts the context. We'll set it to this.
DataContext dataContext = this;

IQueryable<Book> query = from book in dataContext.GetTable<Book>()
						select book;
						
query.Dump();