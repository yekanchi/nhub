<Query Kind="VBStatements">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;System.Transactions.dll</Reference>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.Chapter06to08.Common.dll</Reference>
  <Namespace>LinqInAction.Chapter06to08.Common.SampleClasses.Ch8</Namespace>
</Query>

Dim context As New Ch8DataContext(Connection)
context.Log = Console.Out

Dim changingAuthor = context.Authors.FirstOrDefault()
changingAuthor.FirstName = "Changing"
using ts As new System.Transactions.TransactionScope()
	context.SubmitChanges()
	'Rollback transaction
End Using