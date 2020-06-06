<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

' Create book groups sorted by publisher name

Dim results = New SortedDictionary(Of String, IList(Of String))()

For Each book In SampleData.Books
  Dim publisherBooks As IList(Of String) = Nothing

  If Not results.TryGetValue(book.Publisher.Name, publisherBooks) Then
	publisherBooks = New List(Of String)()
	results(book.Publisher.Name) = publisherBooks
  End If
  publisherBooks.Add(book.Title)
Next

results.Dump()