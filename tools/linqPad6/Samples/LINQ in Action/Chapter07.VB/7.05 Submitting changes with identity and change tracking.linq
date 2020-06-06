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
  <Namespace>LinqInAction.Chapter06to08.Common</Namespace>
</Query>

Dim context1 As New LinqBooksDataContext(Connection.ConnectionString)
Dim context2 As New LinqBooksDataContext(Connection.ConnectionString)

context1.Log = Console.Out
context2.Log = Console.Out

Dim Id As New Guid("92f10ca6-7970-473d-9a25-1ff6cab8f682")

Dim editingSubject = context1.Subjects.Where(Function(s) s.SubjectId = Id).SingleOrDefault()

Console.WriteLine("Before Change:")
editingSubject.Dump()
context2.Subjects.Where(Function(s) s.SubjectId = Id).Dump()

editingSubject.Description = "Testing update"

Console.WriteLine("After Change:")
context1.Subjects.Where(Function(s) s.SubjectId = Id).Dump()
context2.Subjects.Where(Function(s) s.SubjectId = Id).Dump()

context1.SubmitChanges()
Console.WriteLine("After Submit Changes:")
context1.Subjects.Where(Function(s) s.SubjectId = Id).Dump()
context2.Subjects.Where(Function(s) s.SubjectId = Id).Dump()
Dim context3 = New LinqBooksDataContext(Connection.ConnectionString)
context3.Subjects.Where(Function(s) s.SubjectId = Id).Dump()

'Reset values
editingSubject.Description = "Original Value"
context1.SubmitChanges()