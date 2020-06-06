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

//8.3: Optomistic concurrency with Authors using a timestamp.
void Main()
{
	TypedDataContext context = this;
	var authorToChange = context.Authors.First();
	
	authorToChange.FirstName = "Jim";
	authorToChange.LastName = "Wooley";
	
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