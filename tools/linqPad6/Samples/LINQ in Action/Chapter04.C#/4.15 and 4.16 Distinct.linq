<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

static void WithoutDistinct()
{
  var authors = 
    SampleData.Books
      .SelectMany(book => book.Authors)
      .Select(author => author.FirstName+" "+author.LastName);
  authors.Dump("Without Distinct");
}

static void WithDistinct()
{
  var authors =
    SampleData.Books
      .SelectMany(book => book.Authors)
      .Distinct()
      .Select(author => author.FirstName + " " + author.LastName);
  authors.Dump("With Distinct");
}

static void Main()
{
  WithoutDistinct();
  WithDistinct();
}