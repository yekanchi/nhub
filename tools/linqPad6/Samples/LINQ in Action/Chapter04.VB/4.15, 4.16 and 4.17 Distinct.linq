<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Sub WithoutDistinct()
	'Dim authors = _
	'  SampleData.Books _
	'    .SelectMany(Function(book) book.Authors) _
	'    .Select(Function(author) author.FirstName + " " + author.LastName)
	Dim authors = _
		From book In SampleData.Books _
		From author In book.Authors _
		Select author.FirstName + " " + author.LastName
	authors.Dump("Without Distinct")
End Sub

Sub WithDistinct()
	'Dim authors = _
	'  SampleData.Books _
	'    .SelectMany(Function(book) book.Authors) _
	'    .Distinct() _
	'    .Select(Function(author) author.FirstName + " " + author.LastName)
	Dim authors = _
		From book In SampleData.Books _
		From author In book.Authors _
		Select author.FirstName + " " + author.LastName _
		Distinct
	  authors.Dump("With Distinct")
End Sub

Sub Main()
	WithoutDistinct()
	WithDistinct()
End Sub