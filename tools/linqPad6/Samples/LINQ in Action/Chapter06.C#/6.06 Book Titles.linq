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

//6.6: Fetch the list of book titles
//LINQPad abstracts the context. We'll set it to this.
DataContext dataContext = this;

IEnumerable<String> query =
  from book in dataContext.GetTable<Book>()
  select book.Title;
  
query.Dump();