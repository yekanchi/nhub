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

//8.7 Managing transactions through the DataContext
void Main()
{

	LinqBooksDataContext context = new LinqBooksDataContext(Connection.ConnectionString);
	
	MakeConcurrentChanges(context);
	
	try
	{
		context.Connection.Open();
		context.Transaction = context.Connection.BeginTransaction();
		context.SubmitChanges(ConflictMode.ContinueOnConflict);
		context.Transaction.Commit();
	}
	catch (ChangeConflictException)
	{
		Console.WriteLine("Conflict Found. Rolling it back");
		context.Transaction.Rollback();
	}
}

// Define other methods and classes here

 private void MakeConcurrentChanges(LinqBooksDataContext context)
{
  LinqBooksDataContext context1 = new LinqBooksDataContext(Connection.ConnectionString);

  //First user raises the price of each book
  var books1 = context1.Books;
  foreach (var book in books1)
  {
	book.Price += 2;
  }

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