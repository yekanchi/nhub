<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

/// <summary>
/// Simulates code that prepares an ArrayList containing Book objects
/// </summary>
private ArrayList GetArrayList()
{
  return new ArrayList(SampleData.Books);
}

void Main()
{
  ArrayList books = GetArrayList();

  var query =
	from Book book in books
	where book.PageCount > 150
	select new { book.Title, book.Publisher.Name };
  
  query.Dump();
}