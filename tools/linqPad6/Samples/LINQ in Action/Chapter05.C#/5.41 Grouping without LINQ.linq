<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

// Create book groups sorted by publisher name
var results = new SortedDictionary<String, IList<String>>();

foreach (var book in SampleData.Books)
{
  IList<String> publisherBooks;

  if (!results.TryGetValue(book.Publisher.Name, out publisherBooks))
  {
    publisherBooks = new List<String>();
    results[book.Publisher.Name] = publisherBooks;
  }
  publisherBooks.Add(book.Title);
}

results.Dump();