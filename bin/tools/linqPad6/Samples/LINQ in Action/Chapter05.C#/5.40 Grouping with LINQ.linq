<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

// Create book groups sorted by publisher name
var results =
  from book in SampleData.Books
  group book.Title by book.Publisher.Name into publisherBooks
  orderby publisherBooks.Key
  select publisherBooks;

results.Dump();