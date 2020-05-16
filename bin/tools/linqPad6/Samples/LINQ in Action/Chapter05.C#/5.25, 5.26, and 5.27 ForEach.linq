<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

void Main()
{
	// explicit dot notation
	SampleData.Books
	  .Where(book => book.PageCount > 150)
	  .ForEach(book => book.Title.Dump());
	
	Console.WriteLine();
	
	// query expression
	(from book in SampleData.Books
	 where book.PageCount > 150
	 select book)
	  .ForEach(book => book.Title.Dump());
	
	Console.WriteLine();
	
	// statement body
	SampleData.Books
	  .Where(book => book.PageCount > 150)
	  .ForEach(book => {
	    book.Title.Dump();
	  });
	
	Console.WriteLine();
	
	// statement body with update and multiple statements
	SampleData.Books
	  .Where(book => book.PageCount > 150)
	  .ForEach(book => {
	    book.Title += " (long)";
	    book.Title.Dump();
	  });
	}

}// Temporary hack to enable extension methods

public static class Extensions
{
  public static void ForEach<T>(this IEnumerable<T> source, Action<T> func)
  {
    foreach (var item in source)
      func(item);
  }