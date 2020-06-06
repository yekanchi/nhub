<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Function CustomSort(Of TKey)(selector As Func(Of Book, TKey)) As IEnumerable(Of Book)
  Return SampleData.Books.OrderBy(selector)
End Function

Sub Main()
  CustomSort(Function(book) book.Title).Dump("By Title")
  CustomSort(Function(book) book.Publisher.Name).Dump("By Publisher Name")
  CustomSort(Function(book) book.PageCount).Dump("By Page Count")
End Sub