<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

' (1) Set runCount to a custom value to change the tests
const runCount As Integer = 2

Private dictionaryResults As IDictionary(Of String, IList(Of Book))
Private testBooks As List(Of Book)

Sub Main()
  ' (2) Uncomment the test you want to run

'  Console.WriteLine("Without LINQ:")
'  GroupingWithoutLinq()

'  Console.WriteLine("With LINQ:")
'  GroupingWithLinq()
End Sub

Private Sub ClearDictionaryResults()
	dictionaryResults.Clear()
End Sub

Private Sub TestGroupingWithLinq()
	Dim query = _
		From book In testBooks _
		Group book By book.Publisher Into publisherBooks = Group _
		Order By Publisher.Name _
		Select New With {Publisher.Name, .Books = publisherBooks}
	query.ToList()
End Sub

Private Sub TestGroupingWithoutLinq()
	For Each book In testBooks
		Dim publisherBooks As IList(Of Book) = Nothing
		
		If Not dictionaryResults.TryGetValue(book.Publisher.Name, publisherBooks) Then
			publisherBooks = New List(Of Book)()
			dictionaryResults(book.Publisher.Name) = publisherBooks
		End If
		publisherBooks.Add(book)
	Next
End Sub

Sub GroupingWithoutLinq()
	testBooks = GetBooksForPerformance()
	dictionaryResults = New SortedDictionary(Of String, IList(Of Book))()

	Run( _
	  runCount, _
	  New Action(AddressOf ClearDictionaryResults), _
	  New Action(AddressOf TestGroupingWithoutLinq) _
	)
End Sub

Sub GroupingWithLinq()
	testBooks = GetBooksForPerformance()
	dictionaryResults = New SortedDictionary(Of String, IList(Of Book))()

	Run( _
	  runCount, _
	  New Action(AddressOf ClearDictionaryResults), _
	  New Action(AddressOf TestGroupingWithLinq) _
	)
End Sub

Private Function GetBooksForPerformance() As List(Of Book)
	' Seed 123 will return 499357 results for PageCount > 500
	Dim rndBooks = New Random(123)
	Dim rndPublishers = New Random(123)
	Dim publisherCount = SampleData.Publishers.Count()
	
	Dim result = New List(Of Book)()
	For i = 1 To 1000000
		Dim publisher = SampleData.Publishers.Skip(rndPublishers.Next(publisherCount)).First()
		Dim pageCount = rndBooks.Next(1000)
		result.Add(New Book With _
				{ _
				.Title = pageCount.ToString(), _
				.PageCount = pageCount, _
				.Publisher = publisher _
				})
	Next

	Return result
End Function

Private Sub Run(ByVal times As Integer, ByVal prepareFunc As Action, ByVal executeFunc As Action)
  Dim runs = New List(Of Long)(times)

  If Not prepareFunc Is Nothing Then
	prepareFunc()
  End If
  GC.Collect()
  GC.WaitForPendingFinalizers()

  Dim stopwatch = New Stopwatch()
  For i = 1 To times
	stopwatch.Start()
	executeFunc()
	stopwatch.Stop()
	runs.Add(stopwatch.ElapsedMilliseconds)
	stopwatch.Reset()
  Next

  Console.WriteLine("Avg: {0:000}; Min: {1:000}; Max: {2:000}; Runs: {3}{4}", _
	 runs.Average(), runs.Min(), runs.Max(), times, Environment.NewLine)
End Sub