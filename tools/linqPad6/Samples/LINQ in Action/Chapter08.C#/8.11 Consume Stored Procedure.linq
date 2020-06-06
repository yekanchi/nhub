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

//8-11: Using a Stored Procedure to return results
void Main()
{
	
	Guid bookId = new Guid("0737c167-e3d9-4a46-9247-2d0101ab18d1");
	IEnumerable<Book> query =
	  GetBook(bookId,
	  System.Threading.Thread.CurrentPrincipal.Identity.Name);
	
	query.Dump();
}

// Define other methods and classes here
[Function(Name="dbo.GetBook")]
public ISingleResult<Book> GetBook([Parameter(Name="BookId", DbType="UniqueIdentifier")] System.Nullable<System.Guid> bookId, [Parameter(Name="UserName", DbType="NVarChar(50)")] string userName)
{
	IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), bookId, userName);
	return ((ISingleResult<Book>)(result.ReturnValue));
}