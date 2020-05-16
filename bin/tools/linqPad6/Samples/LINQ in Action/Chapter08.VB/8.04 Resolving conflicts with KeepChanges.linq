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
  <Namespace>System.Data.Linq.Mapping</Namespace>
  <Namespace>LinqInAction.Chapter06to08.Common</Namespace>
</Query>

Sub Main
	Dim context = New LinqBooksDataContext(Connection.ConnectionString)
	
	'Make some changes
	MakeConcurrentChanges(context)
	
	Try
		context.SubmitChanges(ConflictMode.ContinueOnConflict)
	Catch e As ChangeConflictException
		Console.WriteLine("Conflicts found resolving with KeepChanges")
		context.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges)
		'Resubmit the merged values
		context.SubmitChanges()
	End Try
End Sub

' Define other methods and classes here
Private Sub MakeConcurrentChanges(ByVal context As LinqBooksDataContext)
	Dim dataContext1 = new LinqBooksDataContext(Connection.ConnectionString)

	'First user raises the price of each book
	Dim books1 As Table(Of LinqInAction.Chapter06to08.Common.Book) = dataContext1.Books
	For Each Book As LinqInAction.Chapter06to08.Common.Book In books1
		Book.Price += 2
	Next

	'Second user lowers the price of each book
	Dim books2 = context.Books
	For Each Book As LinqInAction.Chapter06to08.Common.Book In books2
		Book.Price -= 1
	Next

	'Go ahead and submit the first changes. 
	'The submit using the context passed in to this method will fail.
	dataContext1.SubmitChanges()
End Sub