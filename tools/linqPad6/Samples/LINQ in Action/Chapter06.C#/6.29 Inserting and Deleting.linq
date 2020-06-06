<Query Kind="Program">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
</Query>

//6.29 Adding and removing items from the table
void Main()
{
	//LINQPad abstracts the context. We'll set it to this.
	DataContext dataContext = this;
	
	Table<Book> books = dataContext.GetTable<Book>();
	
	Book newBook = new Book();
	newBook.Price = 40;
	newBook.PubDate = System.DateTime.Today;
	newBook.Title = "LINQ In Action";
	newBook.Publisher = new Guid("4ab0856e-51f3-4b67-9355-8b11510119ba");
	newBook.Subject = new Guid("a0e2a5d7-88c6-4dfe-a416-10eadb978b0b");

	books.InsertOnSubmit(newBook);

	//Rather than commiting the update, we'll just print out the SQL to keep the database values.
	Console.WriteLine(GetChangeText(dataContext));
	//dataContext.SubmitChanges();

	//Now delete it
	books.DeleteOnSubmit(newBook);

	//Rather than commiting the update, we'll just print out the SQL to keep the database values.
	Console.WriteLine(GetChangeText(dataContext));
	//dataContext.SubmitChanges();
}

// Define other methods and classes here

// Helper method to get the change text SQL from the context. 
// This isn't exposed publically, so we'll have to use reflection
// against the private methods. This is for demonstration purposes
// only. It should not be used in production applications and is not
// guaranteed to work in future versions of the framework.
public String GetChangeText(System.Data.Linq.DataContext context)
	{
	  MethodInfo mi = typeof(DataContext).GetMethod("GetChangeText", BindingFlags.NonPublic | BindingFlags.Instance);
	  return mi.Invoke(context, null).ToString();
	}