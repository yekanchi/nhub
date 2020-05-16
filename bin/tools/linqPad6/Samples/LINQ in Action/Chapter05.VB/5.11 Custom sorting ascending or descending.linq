<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Function CustomSort(Of TKey)(selector As Func(Of Book, TKey), ascending As Boolean) As IEnumerable(Of Book)
  Dim books As IEnumerable(Of Book) = SampleData.Books
  Return If(ascending, books.OrderBy(selector), books.OrderByDescending(selector))
End Function

Sub Main()
  CustomSort(Function(book) book.Title, true).Dump("By Title - ascending")
  CustomSort(Function(book) book.Title, false).Dump("By Title - descending")
End Sub