<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

// (1) Set runCount to a custom value to change the tests
const int runCount = 2;

void Main()
{
  // (2) Uncomment the test you want to run

//  Console.WriteLine("Without LINQ:");
//  GroupingWithoutLinq();

//  Console.WriteLine("With LINQ:");
//  GroupingWithLinq();
}

void GroupingWithoutLinq()
{
	var books = GetBooksForPerformance();
	var results = new SortedDictionary<String, IList<Book>>();

	Run(
		runCount,
		delegate { results.Clear(); },
		delegate
		{
			foreach (var book in books)
			{
				IList<Book> publisherBooks;
		
				if (!results.TryGetValue(book.Publisher.Name, out publisherBooks))
				{
					publisherBooks = new List<Book>();
					results[book.Publisher.Name] = publisherBooks;
				}
				publisherBooks.Add(book);
			}
		}
	);
}

void GroupingWithLinq()
{
	var books = GetBooksForPerformance();

	Run(
		runCount,
		null,
		delegate
		{
		  var query =
			from book in books
			group book by book.Publisher.Name into publisherBooks
			orderby publisherBooks.Key
			select publisherBooks;
		  var results = query.ToList();
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