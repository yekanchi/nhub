<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

// Groups books by publisher and subject

var query =
from book in SampleData.Books
group book by new { book.Publisher, book.Subject } into grouping
select new {
  Publisher=grouping.Key.Publisher.Name,
  Subject=grouping.Key.Subject.Name,
  Books=grouping
};

query.Dump();