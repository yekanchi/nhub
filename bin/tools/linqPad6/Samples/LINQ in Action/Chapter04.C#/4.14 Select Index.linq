<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

var books =
  SampleData.Books
    .Select((book, index) => new { index, book.Title })
    .OrderBy(book => book.Title);
books.Dump();