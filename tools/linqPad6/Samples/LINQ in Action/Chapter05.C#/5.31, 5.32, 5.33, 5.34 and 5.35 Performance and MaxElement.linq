<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

// (1) Set runCount to a custom value
const int runCount = 2;

void Main()
{
  // (2) Uncomment the test you want to run

//  Console.WriteLine("foreach:");
//  UseForeach();

//  Console.WriteLine("Sorting and First:");
//  UseSortingAndFirst();

//  Console.WriteLine("Sub-query");
//  UseSubQuery();

//  Console.WriteLine("Two queries");
//  UseTwoQueries();

//  Console.WriteLine("MaxElement custom operator");
//  UseMaxElement();
}

void UseForeach()
{
  var books = GetBooksForPerformance();

  Run(
	runCount,
	null,
	delegate
	{
	  Book maxBook = null;

	  foreach (var book in books)
	  {
		if ((maxBook == null) || (book.PageCount > maxBook.PageCount))
		  maxBook = book;
	  }
	}
  );
}

void UseSortingAndFirst()
{
  var books = GetBooksForPerformance();

  Run(
    runCount,
    null,
    delegate
    {
      var sortedList =
        from book in books
	    orderby book.PageCount descending
        select book;
      var maxBook = sortedList.First();
    }
  );
}

void UseSubQuery()
{
  var books = GetBooksForPerformance();

  Run(
    runCount,
    null,
    delegate
    {
      var maxList =
        from book in books
        where book.PageCount == books.Max(b => b.PageCount)
        select book;
        var maxBook = maxList.First();
    }
  );
}

void UseTwoQueries()
{
  var books = GetBooksForPerformance();

  Run(
	runCount,
	null,
	delegate
	{
      var maxPageCount = books.Max(book => book.PageCount);
      var maxList =
        from book in books
        where book.PageCount == maxPageCount
        select book;
      var maxBook = maxList.First();
	}
  );
}

void UseMaxElement()
{
  var books = GetBooksForPerformance();

  Run(
	runCount,
	null,
	delegate
	{
      var maxBook = books.MaxElement(book => book.PageCount);
	}
  );
}


List<Book> GetBooksForPerformance()
{
  // Seed 123 will return 499357 results for PageCount > 500
  var rndBooks = new Random(123);
  var rndPublishers = new Random(123);
  var publisherCount = SampleData.Publishers.Count();

  var result = new List<Book>();
  for (int i = 0; i < 1000000; i++)
  {
	var publisher = SampleData.Publishers.Skip(rndPublishers.Next(publisherCount)).First();
	var pageCount = rndBooks.Next(1000);
	result.Add(new Book
			   {
				 Title = pageCount.ToString(),
				 PageCount = pageCount,
				 Publisher = publisher
			   });
  }

  return result;
}

void Run(int times, Action prepareFunc, Action executeFunc)
{
  var runs = new List<long>(times);

  if (prepareFunc != null)
    prepareFunc();
  GC.Collect();
  GC.WaitForPendingFinalizers();

  var stopwatch = new Stopwatch();
  for (int i = 0; i < times; i++)
  {
    stopwatch.Start();
    executeFunc();
    stopwatch.Stop();
    runs.Add(stopwatch.ElapsedMilliseconds);
    stopwatch.Reset();
  }

  Console.WriteLine("Avg: {0:000}; Min: {1:000}; Max: {2:000}; Runs: {3}{4}",
    runs.Average(), runs.Min(), runs.Max(), times, Environment.NewLine);
}

}// Temporary hack to enable extension methods

public static class Extensions
{
  public static TElement MaxElement<TElement, TData>(
    this IEnumerable<TElement> source,
    Func<TElement, TData> selector)
    where TData : IComparable<TData>
  {
    if (source == null)
	  throw new ArgumentNullException("source");
    if (selector == null)
      throw new ArgumentNullException("selector");

    Boolean firstElement = true;
    TElement result = default(TElement);
    TData maxValue = default(TData);
    foreach (TElement element in source)
    {
	  var candidate = selector(element);
	  if (firstElement || (candidate.CompareTo(maxValue) > 0))
	  {
	    firstElement = false;
	    maxValue = candidate;
	    result = element;
	  }
    }
    return result;
  }