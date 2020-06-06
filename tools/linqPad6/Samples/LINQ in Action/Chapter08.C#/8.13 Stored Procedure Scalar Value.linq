<Query Kind="Program">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

//8.13 Stored Procedure Scalar Values
void Main()
{
	Guid publisherId = new Guid("851e3294-145d-4fff-a190-3cab7aa95f76");
	Console.WriteLine(String.Format("Books found: {0}",
		BookCountForPublisher(publisherId).ToString()));
}

// Define other methods and classes here
[Function(Name="dbo.BookCountForPublisher")]
public int BookCountForPublisher([Parameter(Name="PublisherId", DbType="UniqueIdentifier")] System.Nullable<System.Guid> publisherId)
{
	IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), publisherId);
	return ((int)(result.ReturnValue));
}