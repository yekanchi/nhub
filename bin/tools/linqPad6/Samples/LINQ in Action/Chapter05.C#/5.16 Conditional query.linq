<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

void ConditionalQuery<TSortKey>(int? minPageCount, String titleFilter, Func<Book, TSortKey> sortSelector)
{
  IEnumerable<Book> query;

  query = SampleData.Books;
  if (minPageCount.HasValue)
	query = query.Where(book => book.PageCount >= minPageCount.Value);
  if (!String.IsNullOrEmpty(titleFilter))
	query = query.Where(book => book.Title.Contains(titleFilter));
  if (sortSelector != null)
	query = query.OrderBy(sortSelector);
  var completeQuery = query.Select(book => new { book.Title, book.PageCount, Publisher=book.Publisher.Name });

  completeQuery.Dump();
}

void Main()
{
  ConditionalQuery(200, null, book => book.PageCount);
}