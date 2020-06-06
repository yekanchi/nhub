<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

' Section 4.4.2

Dim titles1 as IEnumerable(Of String) = SampleData.Books.Select(Function(book) book.Title)
titles1.Dump()

titles1 = _
  From book in SampleData.Books _
  Select book.Title
titles1.Dump()

Dim publishers = _
  From book in SampleData.Books _
  Select book.Publisher
publishers.Dump()

Dim books = _
  From book in SampleData.Books _
  Select New With { book.Title, .Publisher = book.Publisher.Name, book.Authors }
books.Dump()