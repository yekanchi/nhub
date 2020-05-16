<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

''' <summary>
''' Simulates code that prepares an ArrayList containing Book objects
''' </summary>
Function GetArrayList() As ArrayList
	Return New ArrayList(SampleData.Books)
End Function
	
Sub Main
	Dim books As ArrayList = GetArrayList()
	
	Dim query = _
		From book In books.Cast(Of Book)() _
		Where book.PageCount > 150 _
		Select new With { book.Title, book.Publisher.Name }
	
	query.Dump()
End Sub