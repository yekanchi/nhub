<Query Kind="VBProgram">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.Chapter06to08.Common.dll</Reference>
  <Namespace>LinqInAction.Chapter06to08Common</Namespace>
</Query>

Sub Main
	Dim context1 As New TypedDataContext()
	context1.Log = Console.Out
	
	Dim Id As New Guid("92f10ca6-7970-473d-9a25-1ff6cab8f682")
	
	Dim editingSubject = context1.Subjects.Where(Function(s) s.ID = Id).SingleOrDefault()
	
	editingSubject.Description = "Testing update"
	'Apply the changes through a mimiced service
	UpdateSubject(editingSubject)
	
	'Reset values
	editingSubject.Description = "Original Value"
	UpdateSubject(editingSubject)
End Sub

' Define other methods and classes here

Public Shared Sub UpdateSubject(ByVal changingSubject As Subject)
	Dim context As New TypedDataContext()
	Dim existingSubject As Subject = context.Subjects.Where(Function(s) s.ID = changingSubject.ID).FirstOrDefault()
	existingSubject.Name = changingSubject.Name
	existingSubject.Description = changingSubject.Description
	context.SubmitChanges()
End Sub
