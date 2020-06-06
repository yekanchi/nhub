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
  <Namespace>LinqInAction.Chapter06to08.Common.SampleClasses.Ch8</Namespace>
</Query>

LinqInAction.Chapter06to08.Common.LinqBooksDataContext context = new LinqInAction.Chapter06to08.Common.LinqBooksDataContext(this.Connection);
context.Log = Console.Out;

//Get all users from the base instance
Console.WriteLine("All users:");
var query = 
	from user in context.UserBases
	select user.Name;
query.Dump();

Console.WriteLine("Authors: ");
var authors =
	from user in context.UserBases
	where user is LinqInAction.Chapter06to08.Common.AuthorUser
	select user.Name;
authors.Dump();

Console.WriteLine("Publishers: ");
var publishers = 
	from user in context.UserBases.OfType<LinqInAction.Chapter06to08.Common.PublisherUser>()
	select user.Name;
publishers.Dump();
