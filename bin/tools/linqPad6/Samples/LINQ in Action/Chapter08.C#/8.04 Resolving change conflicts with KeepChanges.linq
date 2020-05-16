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

//8.4: Resolving change conflicts with KeepChanges
void Main()
{
	LinqBooksDataContext context = new LinqBooksDataContext(Connection.ConnectionString);
	
	//Make some changes
	MakeConcurrentChanges(context);
	
	try
	{
		context.SubmitChanges(ConflictMode.ContinueOnConflict);
	}
	catch (ChangeConflictException)
	{
		Console.WriteLine("Conflict detected. Resolving with KeepChanges");
		context.ChangeConflicts.ResolveAll(
			RefreshMode.KeepChanges);
		// resubmit the merged values
		context.SubmitChanges();
	}
}

// Define other methods and classes here

 private void MakeConcurrentChanges(LinqBooksDataContext context)
{
  LinqBooksDataContext context1 = new LinqBooksDataContext (Connection.ConnectionString);
  context1.Log = Console.Out;
  
  //First user raises the price of each book
  var books1 = context1.Books;
  foreach (var book in books1)
  {
	book.Price += 2;
  }

  context.Log = Console.Out;  
  //Second user lowers the price of each book
  var books2 = context.Books;
  foreach (var book in books2)
  {
	book.Price -= 1;
  }
  //Go ahead and submit the first changes. 
  //The submit using the context passed in to this method will fail.
  context1.SubmitChanges();
}