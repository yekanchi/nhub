<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Dim books As Book() = { _
  New Book With {.Title = "LINQ in Action"}, _
  New Book With {.Title = "LINQ for Fun"}, _
  New Book With {.Title = "Extreme LINQ"}}

Dim titles = _
  books _
	.Where(Function(book) book.Title.Contains("Action")) _
	.Select(Function(book) book.Title)
	
titles.Dump()