<Query Kind="Program">
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

//8.23 Precompiled query
void Main()
{
	TypedDataContext context = this;
	ExpensiveBooks(context, 35).Dump();
}

// Define other methods and classes here
public static Func<TypedDataContext, decimal, IQueryable<Book>>
	ExpensiveBooks = CompiledQuery.Compile(
		(TypedDataContext context, decimal minimumPrice) =>
			from book in context.Books
			where book.Price >= minimumPrice
			select book);