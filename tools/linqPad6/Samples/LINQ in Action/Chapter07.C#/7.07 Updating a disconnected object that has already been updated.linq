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

//7.7 Updating a disconnected object that has already been updated
void Main()
{
	TypedDataContext context1 = new TypedDataContext();
	  
	Guid Id = new Guid("92f10ca6-7970-473d-9a25-1ff6cab8f682");

	Subject existingSubject = context1.Subjects.Where(s => s.ID == Id).SingleOrDefault();
	Console.WriteLine("Starting name: {0}", existingSubject.Name);

	Subject changingSubject = new Subject { ID = existingSubject.ID };
	changingSubject.Name = @"Testing update";

	using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
	{
		//Apply the changes through a mimiced service
		UpdateSubject(changingSubject);

		//Rollback the change after running the demo
	}
}


public static void UpdateSubject(Subject changingSubject)
{
	TypedDataContext context = new TypedDataContext();

	Subject existingSubject = context.Subjects.Where(subject => subject.ID == changingSubject.ID).FirstOrDefault();
	existingSubject.Name = changingSubject.Name;
	existingSubject.Description = changingSubject.Description;
	context.SubmitChanges();
}