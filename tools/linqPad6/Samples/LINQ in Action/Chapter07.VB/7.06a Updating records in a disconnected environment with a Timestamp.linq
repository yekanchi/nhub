<Query Kind="VBProgram">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
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
	
	Dim cachedAuthor = context1.Authors.First
	Console.WriteLine("Starting Name: {0}", cachedAuthor.FirstName)
	
	'In a real application, this object would now be cached or remoted via a web service.
	'Use a second context to simulate the disconnected environment.
	Using ts As New System.Transactions.TransactionScope
		UpdateAuthor(cachedAuthor)
		Console.WriteLine("Updated Name: {0}", cachedAuthor.FirstName)
		'rollback changes
	End Using
End Sub
	
' Use second context to simulate disconnected environment
Public Sub UpdateAuthor(ByVal cachedAuthor As Author)
	Dim context As New TypedDataContext()
	context.Log = Console.Out
	
	cachedAuthor.LastName = "Testing update"
	context.Authors.Attach(cachedAuthor, True)
	
	context.SubmitChanges()
End Sub

' Define other methods and classes here