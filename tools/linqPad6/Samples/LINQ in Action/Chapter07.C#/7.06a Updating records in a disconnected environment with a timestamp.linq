<Query Kind="Program">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;System.Transactions.dll</Reference>
</Query>

//7.6a Updating records in a disconnected environment with a timestamp
void Main()
{
  	// LINQPad wraps the context via a TypedDataContext. 
	// We'll use that instead of the LinqBooksDataContext
	// used in the LINQ In Action book text.
	TypedDataContext context1 = new TypedDataContext();

  /* Objects can only be attached to a single context at any given time. 
   * This is done to avoid the potential to update child objects erroneously. 
   * For the purposes of this example, we purposefully set up context1 up so that
   * it wouldn’t track the changes by setting the ObjectTrackingEnabled to false.
   * Attempting to attach an object to a second context will result in a NotSupportedException.  */
  context1.ObjectTrackingEnabled = false;

  Author cachedAuthor = context1.Authors.First();
  Console.WriteLine("Starting name: {0}", cachedAuthor.FirstName);

  // In a real application, this object would now be cached or remoted via a web service
  // Use second context to simulate disconnected environment

  using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
  {
	UpdateAuthor(cachedAuthor);
	Console.WriteLine("Updated name: {0}", cachedAuthor.FirstName);
	//Rollback the change after running the demo
  }
}

public void UpdateAuthor(Author cachedAuthor)
{
  TypedDataContext context = new TypedDataContext();
  cachedAuthor.FirstName = @"Testing update";
  context.Authors.Attach(cachedAuthor, true);

  context.SubmitChanges();
}

