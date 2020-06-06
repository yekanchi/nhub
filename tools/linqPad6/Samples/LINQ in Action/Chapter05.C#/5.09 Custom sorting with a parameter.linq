<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

IEnumerable<Book> CustomSort<TKey>(Func<Book, TKey> selector)
{
  return SampleData.Books.OrderBy(selector);
}

void Main()
{
  CustomSort(book => book.Title).Dump("By Title");
  CustomSort(book => book.Publisher.Name).Dump("By Publisher Name");
  CustomSort(book => book.PageCount).Dump("By Page Count");
}