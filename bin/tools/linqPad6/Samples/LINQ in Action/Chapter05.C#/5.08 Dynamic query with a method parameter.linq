<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

private void ParameterizedQuery(int minPageCount)
{
  var books =
	from book in SampleData.Books
	where book.PageCount >= minPageCount
	select book;

  Console.WriteLine("Books with at least {0} pages: {1}",
	minPageCount, books.Count());
}
	
void Main()
{
  ParameterizedQuery(200);
  ParameterizedQuery(50);
}