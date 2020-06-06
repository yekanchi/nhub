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

Sub Main
	Dim context1 As New TypedDataContext()
	context1.Log = Console.Out
	
	'Objects can only be attached to a single context at any given time.
	'This is done to avoid the potential to update child objects erroneously.
	'For the purposes of this example, we purposefully set up context 1 so that 
	'it wouldn't track the changes by setting the ObjectTrackingEnabled to false.
	'Attempting to attach an object to a second context will result in a NotSupportedException
	context1.ObjectTrackingEnabled = False
	
	Dim cachedSubject As Subject = context1.Subjects.First
	Dim newVersion As New Subject With {.Name = "Testing a change", _
										.ID = cachedSubject.ID, _
										.Description = cachedSubject.Description}
	
	Console.WriteLine("Starting subject Name: {0}", cachedSubject.Name)
	
	Using ts As New System.Transactions.TransactionScope
		UpdateSubject(cachedSubject, newVersion)
	
		'rollback changes
	 End Using
End Sub

Public Sub UpdateSubject(ByVal cachedVersion As Subject, ByVal newVersion As Subject)
	Dim context As New TypedDataContext
	context.Log = Console.Out
	context.Subjects.Attach(newVersion, cachedVersion)
	context.SubmitChanges()
	
	Console.Write("New value: ")
	Dim query = From s In context.Subjects Where s.ID = newVersion.ID Select s
	query.Dump()

End Sub