<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Private Sub ParameterizedQuery(ByVal minPageCount As Integer)
  Dim books = _
    From book In SampleData.Books _
    Where book.PageCount >= minPageCount _
    Select book

  Console.WriteLine("Books with at least {0} pages: {1}", _
	minPageCount, books.Count())
End Sub

Sub Main()
  ParameterizedQuery(200)
  ParameterizedQuery(50)
End Sub