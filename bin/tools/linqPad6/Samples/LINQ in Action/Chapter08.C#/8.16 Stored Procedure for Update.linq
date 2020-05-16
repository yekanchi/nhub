<Query Kind="Statements">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.Chapter06to08.Common.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Transactions.dll</Reference>
  <Namespace>LinqInAction.Chapter06to08.Common.SampleClasses.Ch8</Namespace>
</Query>

//8.16 Stored Procedure for Update
Ch8DataContext context = new Ch8DataContext(this.Connection);
context.Log = Console.Out;

var changingAuthor = context.Authors.FirstOrDefault();
changingAuthor.FirstName = "Changing";
using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
{
	context.SubmitChanges();
	//Rollback transaction
}