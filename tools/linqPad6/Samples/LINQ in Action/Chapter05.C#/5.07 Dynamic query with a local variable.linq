<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

int minPageCount = 0;

var books =
  from book in SampleData.Books
  where book.PageCount >= minPageCount
  select book;

minPageCount = 200;
Console.WriteLine("Books with at least {0} pages: {1}",
  minPageCount, books.Count());
minPageCount = 50;
Console.WriteLine("Books with at least {0} pages: {1}",
  minPageCount, books.Count());