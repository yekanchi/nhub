<Query Kind="VBProgram">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;System.Transactions.dll</Reference>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

'NOTE: You need to enable the MSDTC on the client as the transaction is escalated to a distributed transaction due to the way that the connections are made.

Sub Main
   Dim context1 As New TypedDataContext()
	context1.ObjectTrackingEnabled = False
	
	context1.Log = Console.Out
	
	Dim editingAuthor As Author = context1.Authors.First
	Console.WriteLine("Starting Author Name: {0}", editingAuthor.LastName)
	
	Using ts As New System.Transactions.TransactionScope
		UpdateAuthor(editingAuthor.ID, editingAuthor.FirstName, "Testing change", editingAuthor.WebSite, editingAuthor.TimeStamp)
	
	
		'Refetch it and show the database value now
		Dim authors = From a In context1.Authors Where a.ID = editingAuthor.ID Select a
		authors.Dump()
		'rollback changes
	End Using
End Sub


Public Sub UpdateAuthor(ByVal id As Guid, ByVal firstName As String, _
						ByVal lastName As String, ByVal webSite As String, _
						ByVal timeStamp As System.Data.Linq.Binary)
  Dim context As New TypedDataContext()
  context.Log = Console.Out
  context.Authors.Attach(New Author With { _
							  .ID = id, _
							  .FirstName = firstName, _
							  .LastName = lastName, _
							  .WebSite = webSite, _
							  .TimeStamp = timeStamp}, _
					   True)
  context.SubmitChanges()

End Sub