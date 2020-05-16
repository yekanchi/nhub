<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Dim books As List(Of Book) = New List(Of Book)()
books.Add(New Book With {.Title = "LINQ in Action"})
books.Add(New Book With {.Title = "LINQ for Fun"})
books.Add(New Book With {.Title = "Extreme LINQ"})

Dim titles = _
  books _
	.Where(Function(book) book.Title.Contains("Action")) _
	.Select(Function(book) book.Title)

titles.Dump()