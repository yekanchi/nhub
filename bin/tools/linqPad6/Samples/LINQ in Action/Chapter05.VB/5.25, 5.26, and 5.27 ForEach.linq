<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Sub Main()
	' ForEach cannot be used in all cases in VB because it requires a statement lambda
	'  and VB.NET 9.0 does not offer support for statement lambdas.
	'  The samples we have in C# cannot be converted to VB.

	' The calls to ForEach in the statements below produce the following
	'  error at compile-time: "Expression does not produce a value".

	'SampleData.Books _
	'  .Where(Function(Book) Book.PageCount > 150) _
	'  .ForEach(Function(book) Console.WriteLine(book.Title))

	'Dim query = _
	'  From book In SampleData.Books _
	'  Where book.PageCount > 150 _
	'  Select book
	'query.ForEach(Function(book) Console.WriteLine(book.Title))

	' The following statements compile and work fine though!
	
	SampleData.Books _
	  .Where(Function(Book) Book.PageCount > 150) _
	  .ForEach(Function(book) book.Title.Dump())

    Console.WriteLine()

	Dim query = _
	  From book In SampleData.Books _
	  Where book.PageCount > 150 _
	  Select book
	query.ForEach(Function(book) book.Title.Dump())

End Sub

End Class ' Temporary hack to enable extension methods

Module Extensions

  <System.Runtime.CompilerServices.Extension()> _
  Public Sub ForEach(Of T)(ByVal source As IEnumerable(Of T), ByVal func As Action(Of T))
	For Each item In source
	  func(item)
	Next
  End Sub

End Module

Class Hack ' Temporary hack to enable extension methods