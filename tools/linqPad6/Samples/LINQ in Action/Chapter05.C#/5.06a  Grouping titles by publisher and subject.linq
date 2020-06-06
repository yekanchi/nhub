<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

// Groups books by subject and returns the title and publisher of each book

var query =
  from book in SampleData.Books
  group new {book.Title, book.Publisher.Name} by book.Subject into grouping
  select new {Subject=grouping.Key.Name, Books=grouping};

query.Dump();