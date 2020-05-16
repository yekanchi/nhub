<Query Kind="VBStatements">
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

Dim context As New LinqInAction.Chapter06to08.Common.LinqBooksDataContext(Connection)
context.Log = Console.Out

Console.WriteLine("All Users")
'Get all users from the base instance
Dim Query = From user In context.UserBases _
	Select user.Name
	
Query.Dump()

Console.WriteLine("Authors: ")
'Get only the authors
Dim authors = _
	From user In context.UserBases _
	Where TypeOf user Is LinqInAction.Chapter06to08.Common.AuthorUser _
	Select user.Name
authors.Dump()

Console.WriteLine("Publishers")
'Get the publishers using the OfType extension method
Dim publishers = _
	From user In context.UserBases.OfType(Of LinqInAction.Chapter06to08.Common.PublisherUser)() _
	Select user.Name
publishers.Dump()

