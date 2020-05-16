<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Dim minPageCount As Integer = 0

Dim books = _
  From book In SampleData.Books _
  Where book.PageCount >= minPageCount _
  Select book

minPageCount = 200
Console.WriteLine("Books with at least {0} pages: {1}", _
  minPageCount, books.Count())
minPageCount = 50
Console.WriteLine("Books with at least {0} pages: {1}", _
  minPageCount, books.Count())