<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

enum Test { FilterOnInt, FilterOnString, Sort, Join }

// (1) Set runCount and test to custom values to change the tests
const int runCount = 2;
const Test test = Test.FilterOnInt;

void Main()
{
  // (2) Uncomment the test you want to run

//  Console.WriteLine("foreach:");
//  UseForeach();

//  Console.WriteLine("for:");
//  UseFor();

//  Console.WriteLine("List<T>.FindAll");
//  UseListFindAll();

//  Console.WriteLine("List<T>.ForEach");
//  UseListForEach();

//  Console.WriteLine("LINQ with ToList");
//  UseLinqWithToList();

//  Console.WriteLine("LINQ with presized list");
//  UseLinqWithPresizedList();
}

void UseForeach()
{
	var books = GetBooksForPerformance();

	if (test == Test.FilterOnInt)
	{
		var results = new List<Book>(499357);
		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			foreach (var book in books)
			{
			  if (book.PageCount > 500)
				results.Add(book);
			}
		  }
		);
	}
	else if (test == Test.FilterOnString)
	{
		var results = new List<Book>(499357);
		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			foreach (var book in books)
			{
			  if (book.Title.StartsWith("1"))
				results.Add(book);
			}
		  }
		);
	}
	else if (test == Test.Sort)
	{
		var results = new List<Book>(499357);
		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			foreach (var book in books)
			{
			  if (book.PageCount > 500)
				results.Add(book);
			}
			results.Sort((book1, book2) => book1.Title.CompareTo(book2.Title));
		  }
		);
	}
	else if (test == Test.Join)
	{
		var results = new List<Book>(499357);
		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			foreach (var publisher in SampleData.Publishers)
			  foreach (var book in books)
			  {
				if ((book.Publisher == publisher) && (book.PageCount > 500))
				  results.Add(book);
			  }
		  }
		);
	}
	else
	{
		throw new NotImplementedException();
	}
}

void UseFor()
{
	var books = GetBooksForPerformance();

	if (test == Test.FilterOnInt)
	{
		var results = new List<Book>(499357);
		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			for (int i = 0; i < books.Count; i++)
			{
			  Book book = books[i];
			  if (book.PageCount > 500)
				results.Add(book);
			}
		  }
		);
	}
	else if (test == Test.FilterOnString)
	{
		var results = new List<Book>(499357);
		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
		    for (int i = 0; i < books.Count; i++)
		    {
		      Book book = books[i];
		      if (book.Title.StartsWith("1"))
			    results.Add(book);
		    }
		  }
		);
	}
	else
	{
		throw new NotImplementedException();
	}
}

// Does not return titles
// Does not pre-size the list
void UseListFindAll()
{
	var books = GetBooksForPerformance();

	if (test == Test.FilterOnInt)
	{
		Run(
		  runCount,
		  null,
		  delegate
		  {
			var results = books.FindAll(book => book.PageCount > 500);
		  }
		);
	  }
	  else if (test == Test.FilterOnString)
	  {
		Run(
		  runCount,
		  null,
		  delegate
		  {
			var results = books.FindAll(book => book.Title.StartsWith("1"));
		  }
		);
	  }
	  else
	  {
		throw new NotImplementedException();
	  }
}

void UseListForEach()
{
	var books = GetBooksForPerformance();

	if (test == Test.FilterOnInt)
	{
		var results = new List<Book>(499357);
		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			books.ForEach(delegate(Book book)
			{
			  if (book.PageCount > 500)
				results.Add(book);
			});
		  }
		);
	  }
	  else if (test == Test.FilterOnString)
	  {
		var results = new List<Book>(499357);
		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			books.ForEach(book =>
			{
			  if (book.Title.StartsWith("1"))
				results.Add(book);
			});
		  }
		);
	  }
	  else
	  {
		throw new NotImplementedException();
	  }
}

void UseLinqWithToList()
{
	var books = GetBooksForPerformance();

	if (test == Test.FilterOnInt)
	{
		Run(
		  runCount,
		  null,
		  delegate
		  {
			var query =
			  from book in books
			  where book.PageCount > 500
			  select book.Title;
			query.ToList();
		  }
		);
	}
	else if (test == Test.FilterOnString)
	{
		Run(
		  runCount,
		  null,
		  delegate
		  {
			var query =
			  from book in books
			  where book.Title.StartsWith("1")
			  select book;
			query.ToList();
		  }
		);
	}
	else
	{
		throw new NotImplementedException();
	}
}

void UseLinqWithPresizedList()
{
	var books = GetBooksForPerformance();

	if (test == Test.FilterOnInt)
	{
		var results = new List<Book>(499357);

		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			var query =
			  from book in books
			  where book.PageCount > 500
			  select book;
			foreach (var book in query)
			{
			  results.Add(book);
			}
		  }
		);
	}
	else if (test == Test.FilterOnString)
	{
		var results = new List<Book>(499357);

		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			var query =
			  from book in books
			  where book.Title.StartsWith("1")
			  select book;
			foreach (var book in query)
			{
			  results.Add(book);
			}
		  }
		);
	}
	else if (test == Test.Sort)
	{
		var results = new List<Book>(499357);

		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			var query =
			  from book in books
			  where book.PageCount > 500
			  orderby book.Title
			  select book;
			foreach (var book in query)
			{
			  results.Add(book);
			}
		  }
		);
	}
	else if (test == Test.Join)
	{
		var results = new List<Book>(499357);

		Run(
		  runCount,
		  delegate { results.Clear(); },
		  delegate
		  {
			var query =
			  from publisher in SampleData.Publishers
			  join book in books on publisher equals book.Publisher
			  where book.PageCount > 500
			  select book;
			foreach (var book in query)
			{
			  results.Add(book);
			}
		  }
		);
	}
	else
	{
		throw new NotImplementedException();
	}
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