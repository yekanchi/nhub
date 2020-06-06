<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

' Groups book titles by publisher and subject

Dim query = _
  From book In SampleData.Books _
  Group book.Title By book.Publisher, book.Subject Into grouping = Group _
  Select New With { _
	.Publisher = Publisher.Name, _
	.Subject = Subject.Name, _
	.Titles = grouping _
  }

query.Dump()