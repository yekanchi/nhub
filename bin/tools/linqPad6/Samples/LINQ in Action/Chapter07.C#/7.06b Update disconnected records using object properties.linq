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

//7.6b Updating records in a disconnected environment passing in object properties
//NOTE: You need to enable the MSDTC on the client as the transaction is escalated to a distributed transaction due to the way that the connections are made.
void Main()
{
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
		UpdateAuthor(cachedAuthor.ID, cachedAuthor.FirstName, "Testing change", cachedAuthor.WebSite, cachedAuthor.TimeStamp);
		//refetch it and show the database value now
		(from a in context1.Authors where a.ID == cachedAuthor.ID select a).Dump();

		//Rollback the change after running the demo
	}
}

public void UpdateAuthor(Guid id, String firstName, String lastName, String webSite, System.Data.Linq.Binary timeStamp)
{
	TypedDataContext context = new TypedDataContext();
	context.Log = Console.Out;
	context.Authors.Attach(new Author
	{
		ID = id,
		FirstName = firstName,
		LastName = lastName,
		WebSite = webSite,
		TimeStamp = timeStamp
	}, true);
	
	context.SubmitChanges();
}