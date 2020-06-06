<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

// Query expression with operators
var authors1 =
  from distinctAuthor in (
  	from book in SampleData.Books
  	where book.Title.Contains("LINQ")
  	from author in book.Authors.Take(1)
  	select author)
	  .Distinct()
  select new {distinctAuthor.FirstName, distinctAuthor.LastName};

authors1.Dump("Query expression with operators");

// Same query without query expression
var authors2 =
  SampleData.Books
	  .Where(book => book.Title.Contains("LINQ"))
	  .SelectMany(book => book.Authors.Take(1))
	  .Distinct()
	  .Select(author => new {author.FirstName, author.LastName});

authors2.Dump("Without query expression");