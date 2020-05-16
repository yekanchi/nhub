<Query Kind="Program">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;System.Transactions.dll</Reference>
</Query>

//7-6c Updating with attach old and new
void Main()
{
	TypedDataContext context1 = new TypedDataContext();
	
	  /* Objects can only be attached to a single context at any given time. 
	   * This is done to avoid the potential to update child objects erroneously. 
	   * For the purposes of this example, we purposefully set up context1 up so that
	   * it wouldn’t track the changes by setting the ObjectTrackingEnabled to false.
	   * Attempting to attach an object to a second context will result in a NotSupportedException.  */
	context1.ObjectTrackingEnabled = false;

	Subject cachedSubject = context1.Subjects.First();
	Subject newVersion = new Subject
	  {
		Name = "Testing a change",
		ID = cachedSubject.ID,
		Description = cachedSubject.Description
	  };

	Console.WriteLine("Starting name: {0}", cachedSubject.Name);

	// In a real application, this object would now be cached or remoted via a web service
	// Use second context to simulate disconnected environment
	using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
	{
		UpdateSubject(cachedSubject, newVersion);
		//Rollback the change after running the demo
	}
}

public void UpdateSubject(Subject cachedVersion, Subject newVersion)
{
	TypedDataContext context = new TypedDataContext();
	context.Subjects.Attach(newVersion, cachedVersion);
	context.SubmitChanges();

	Console.WriteLine("New value: ");
	(from s in context.Subjects where s.ID == newVersion.ID select s).Dump();
}