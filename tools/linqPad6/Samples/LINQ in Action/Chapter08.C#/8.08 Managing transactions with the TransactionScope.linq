<Query Kind="Program">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;System.Transactions.dll</Reference>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.Chapter06to08.Common.dll</Reference>
  <Namespace>LinqInAction.Chapter06to08.Common</Namespace>
</Query>

//8.8 Managing transactions with the TransactionScope
void Main()
{

	LinqBooksDataContext context = new LinqBooksDataContext(this.Connection.ConnectionString);
	
	MakeConcurrentChanges(context);
	
	using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
	{
		context.SubmitChanges(ConflictMode.ContinueOnConflict);
		scope.Complete();
	}
}

// Define other methods and classes here

private void MakeConcurrentChanges(LinqBooksDataContext context)
{
  LinqBooksDataContext context1 = new LinqBooksDataContext(this.Connection.ConnectionString);

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