<Query Kind="Program">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.Chapter06to08.Common.dll</Reference>
  <Namespace>LinqInAction.Chapter06to08.Common</Namespace>
</Query>

//8.2: Default concurrency implementation
void Main()
{
	LinqBooksDataContext context = new LinqBooksDataContext(Connection.ConnectionString);
	var mostExpensiveBook = (from book in context.Books
							orderby book.Price descending
							select book).First();
	decimal discount = .1M;
	mostExpensiveBook.Price -= mostExpensiveBook.Price * discount;
	
	//context.SubmitChanges();
	
	//Rather than committing a change, just view the SQL that would have been used.
	Console.WriteLine(GetChangeText(context));
}

// Define other methods and classes here

// Helper method to get the change text SQL from the context. 
// This isn't exposed publically, so we'll have to use reflection
// against the private methods. This is for demonstration purposes
// only. It should not be used in production applications and is not
// guaranteed to work in future versions of the framework.
public static String GetChangeText(System.Data.Linq.DataContext context)
{
  MethodInfo mi = typeof(DataContext).GetMethod("GetChangeText", BindingFlags.NonPublic | BindingFlags.Instance);
  return mi.Invoke(context, null).ToString();
}