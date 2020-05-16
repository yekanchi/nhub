<Query Kind="Statements">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.Chapter06to08.Common.dll</Reference>
  <Namespace>LinqInAction.Chapter06to08.Common</Namespace>
</Query>

//7.4: Identity management and change tracking
	  
LinqBooksDataContext context1 = new LinqBooksDataContext(Connection.ConnectionString);
LinqBooksDataContext context2 = new LinqBooksDataContext(Connection.ConnectionString);

Guid Id = new Guid("92f10ca6-7970-473d-9a25-1ff6cab8f682");

LinqInAction.Chapter06to08.Common.Subject editingSubject = context1.Subjects.Where(s => s.SubjectId == Id).SingleOrDefault();

Console.WriteLine("Before Change:");
editingSubject.Dump();
context2.Subjects.Where(s => s.SubjectId == Id).Dump();

editingSubject.Description = @"Testing update";

Console.WriteLine("After Change:");
context1.Subjects.Where(s => s.SubjectId == Id).Dump();
context2.Subjects.Where(s => s.SubjectId == Id).Dump();