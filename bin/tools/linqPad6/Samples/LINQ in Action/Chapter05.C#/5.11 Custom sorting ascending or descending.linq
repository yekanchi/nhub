<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

IEnumerable<Book> CustomSort<TKey>(Func<Book, TKey> selector, Boolean ascending)
{
  IEnumerable<Book> books = SampleData.Books;
  return ascending ? books.OrderBy(selector) : books.OrderByDescending(selector);
}

void Main()
{
  CustomSort(book => book.Title, true).Dump("By Title - ascending");
  CustomSort(book => book.Title, false).Dump("By Title - descending");
}