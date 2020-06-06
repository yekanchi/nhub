<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

' Section 4.4.4

Dim titles As IEnumerable(Of String) = SampleData.Books.Select(Function(book) book.Title)

Dim array As String() = titles.ToArray()
array.Dump("ToArray")

Dim list As List(Of String) = titles.ToList()
list.Dump("ToList")

Console.WriteLine("ToDictionary:")
Dim isbnRef As Dictionary(Of String, Book) = SampleData.Books.ToDictionary(Function(book) book.Isbn)
Dim linqRules As Book = isbnRef("0-111-77777-2")
Console.WriteLine("Book found: " + linqRules.ToString())