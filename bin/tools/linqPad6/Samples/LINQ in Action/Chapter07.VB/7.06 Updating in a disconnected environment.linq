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
	Dim context1 = New TypedDataContext
	context1.Log = Console.Out
	
	'Objects can only be attached to a single context at any given time.
	'This is done to avoid the potential to update child objects erroneously.
	'For the purposes of this example, we purposefully set up context 1 so that 
	'it wouldn't track the changes by setting the ObjectTrackingEnabled to false.
	'Attempting to attach an object to a second context will result in a NotSupportedException
	context1.ObjectTrackingEnabled = False
	
	Dim cachedSubject = context1.Subjects.First
	Console.WriteLine("Starting Name: {0}", cachedSubject.Name)
	
	'In a real application, this object would now be cached or remoted via a web service.
	'Use a second context to simulate the disconnected environment.
	Using ts As New System.Transactions.TransactionScope
		UpdateSubject(cachedSubject)
	
		Console.WriteLine("Updated Name: {0}", cachedSubject.Name)
		'rollback changes
	End Using
End Sub

' Define other methods and classes here
Public Sub UpdateSubject(ByVal cachedSubject As Subject)
	Dim context = New TypedDataContext
	context.Log = Console.Out
	context.Subjects.Attach(cachedSubject)
	cachedSubject.Name = "Testing update"
	context.SubmitChanges()
End Sub