<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

// Section 4.4.4

IEnumerable<String> titles = SampleData.Books.Select(book => book.Title);

String[] array = titles.ToArray();
array.Dump("ToArray");

List<String> list = titles.ToList();
list.Dump("ToList");

Console.WriteLine("ToDictionary:");
Dictionary<String, Book> isbnRef = SampleData.Books.ToDictionary(book => book.Isbn);
Book linqRules = isbnRef["0-111-77777-2"];
Console.WriteLine("Book found: " + linqRules);