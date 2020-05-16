<Query Kind="VBProgram">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Sub Main
	Dim dataContext As DataContext = Me
	
	Dim books As Table(Of Book) = dataContext.GetTable(Of Book)()
	
	Dim newBook As New Book()
	newBook.Price = 40
	newBook.PubDate = System.DateTime.Today
	newBook.title = "LINQ In Action"
	newBook.Publisher = New Guid("4ab0856e-51f3-4b67-9355-8b11510119ba")
	newBook.Subject = New Guid("a0e2a5d7-88c6-4dfe-a416-10eadb978b0b")

	books.InsertOnSubmit(newBook)

	Console.WriteLine(GetChangeText(dataContext))

	'Now delete it
	books.DeleteOnSubmit(newBook)

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