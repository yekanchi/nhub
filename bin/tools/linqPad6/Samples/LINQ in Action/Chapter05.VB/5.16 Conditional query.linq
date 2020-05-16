<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Private Sub ConditionalQuery(Of TSortKey)(ByVal minPageCount As Integer?, ByVal titleFilter As String, ByVal sortSelector As Func(Of Book, TSortKey))
	Dim query As IEnumerable(Of Book)
	
	query = SampleData.Books
	If minPageCount.HasValue Then
		query = query.Where(Function(book) book.PageCount >= minPageCount.Value)
	End If
	If Not String.IsNullOrEmpty(titleFilter) Then
		query = query.Where(Function(book) book.Title.Contains(titleFilter))
	End If
	If Not sortSelector Is Nothing Then
		query = query.OrderBy(sortSelector)
	End If
	Dim completeQuery = query.Select(Function(book) New With {book.Title, book.PageCount, .Publisher = book.Publisher.Name})
	
	completeQuery.Dump()
End Sub

Sub Main()
	ConditionalQuery(200, Nothing, Function(book) book.PageCount)
End Sub