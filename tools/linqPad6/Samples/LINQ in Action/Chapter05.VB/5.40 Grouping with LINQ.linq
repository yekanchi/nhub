<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

' Create book groups sorted by publisher name

Dim results = _
  From book In SampleData.Books _
  Group book.Title By book.Publisher Into publisherBooks = Group _
  Order By Publisher.Name _
  Select New With {Publisher.Name, .Books = publisherBooks}

results.Dump()