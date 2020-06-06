<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Dim books = _
  SampleData.Books _
	.Select(Function(book, index) New With {index, book.Title}) _
	.OrderBy(Function(book) book.Title)
books.Dump()