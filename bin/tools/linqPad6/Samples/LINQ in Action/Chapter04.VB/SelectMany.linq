<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

' Section 4.4.2

' With Select
Dim tmp1 As IEnumerable(Of IEnumerable(Of Author)) = SampleData.Books.Select(Function(book) book.Authors)
tmp1.Dump("With Select")
For Each authors In tmp1
	For Each author In authors
    	author.LastName.Dump()
	Next
Next

' With SelectMany
Dim tmp2 As IEnumerable(Of Author) = SampleData.Books.SelectMany(Function(book) book.Authors)
tmp2.Dump("With SelectMany")
For Each author In tmp2
  author.LastName.Dump()
Next

Dim bookAuthors = _
  From book In SampleData.Books _
  From author In book.Authors _
  Select author.LastName
bookAuthors.Dump("With a double from")