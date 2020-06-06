<Query Kind="VBProgram">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Sub Main
	Dim dataContext As DataContext = Me
	
	Dim ExpensiveBooks = _
		From b in dataContext.GetTable(Of Book)()  _
		Where b.Price > 30 _
		Select b
		
	For Each _book As Book In ExpensiveBooks
		_book.Price -=5
	Next
	
	'We would typically submit these changes to the database using:
	'DataContext.SubmitChanges()
	'
	'In this case, we will just get the SQL that would have been issued 
	Console.WriteLine(GetChangeText(dataContext))
End Sub

' Define other methods and classes here
' Helper method to get the change text SQL from the context. 
' This isn't exposed publically, so we'll have to use reflection
' against the private methods. This is for demonstration purposes
' only. It should not be used in production applications and is not
' guaranteed to work in future versions of the framework.
Public Function GetChangeText(ByVal context As System.Data.Linq.DataContext) As String
	Dim mi As MethodInfo = GetType(DataContext).GetMethod("GetChangeText", BindingFlags.NonPublic Or BindingFlags.Instance)
	Return mi.Invoke(context, Nothing).ToString()
End Function