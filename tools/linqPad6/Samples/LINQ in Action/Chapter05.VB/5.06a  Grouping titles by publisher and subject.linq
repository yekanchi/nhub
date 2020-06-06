<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

' Groups books by subject and returns the title and publisher of each book

Dim query = _
  From book In SampleData.Books _
  Group New With {book.Title, book.Publisher.Name} By book.Subject Into grouping = Group _
  Select New With {.Subject = Subject.Name, .Books = grouping}

query.Dump()