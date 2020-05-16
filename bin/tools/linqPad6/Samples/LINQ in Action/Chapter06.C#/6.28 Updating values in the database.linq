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

void Main()
{
	//6.28: Updating values and committing them to the database
	//LINQPad abstracts the context. We'll set it to this.
	DataContext dataContext = this;
	
	var ExpensiveBooks = 
		from b in dataContext.GetTable<Book>()
		where b.Price > 30
		select b;
		
	foreach ( Book b in ExpensiveBooks)
	{
		b.Price -= 5;
	}
	
	//Rather than commiting this update, we'll just output the SQL that holds the database values.
	Console.WriteLine(GetChangeText(dataContext));
	//dataContext.SubmitChanges();

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