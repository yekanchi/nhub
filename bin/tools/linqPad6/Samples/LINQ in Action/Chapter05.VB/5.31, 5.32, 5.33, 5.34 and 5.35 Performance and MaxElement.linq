<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

' (1) Set runCount to a custom value
Const runCount As Integer = 2

Private testBooks As List(Of Book)

Sub Main()
  ' (2) Uncomment the test you want to run

'  Console.WriteLine("foreach:")
'  UseForeach()

'  Console.WriteLine("Sorting and First:")
'  UseSortingAndFirst()

'  Console.WriteLine("Sub-query")
'  UseSubQuery()

'  Console.WriteLine("Two queries")
'  UseTwoQueries()

'  Console.WriteLine("MaxElement custom operator")
'  UseMaxElement()
End Sub

Private Sub TestMaxElementWithoutLinq()
	Dim maxBook As Book = Nothing
	
	For Each book In testBooks
		If (maxBook Is Nothing) Then
			maxBook = book
		ElseIf book.PageCount > maxBook.PageCount Then
			maxBook = book
		End If
	Next
End Sub

Sub UseForeach()
	testBooks = GetBooksForPerformance()

	Run( _
	  runCount, _
	  Nothing, _
	  New Action(AddressOf TestMaxElementWithoutLinq) _
	)
End Sub

Private Sub TestMaxElementWithOrderByAndFirst()
	Dim sortedList = _
		From book In testBooks _
		Order By book.PageCount Descending _
		Select book
	Dim maxBook = sortedList.First()
End Sub

Sub UseSortingAndFirst()
	testBooks = GetBooksForPerformance()

	Run( _
	  runCount, _
	  Nothing, _
	  New Action(AddressOf TestMaxElementWithOrderByAndFirst) _
	)
End Sub

Private Sub TestMaxElementWithSubQuery()
	Dim maxList = _
		From book In testBooks _
		Where book.PageCount = testBooks.Max(Function(b) b.PageCount) _
		Select book
	Dim maxBook = maxList.First()
End Sub

Sub UseSubQuery()
	testBooks = GetBooksForPerformance()

	Run( _
	  runCount, _
	  Nothing, _
	  New Action(AddressOf TestMaxElementWithSubQuery) _
	)
End Sub

Private Sub TestMaxElementWithTwoQueries()
	Dim maxPageCount = testBooks.Max(Function(book) book.PageCount)
	Dim maxList = _
		From book In testBooks _
		Where book.PageCount = maxPageCount _
		Select book
	Dim maxBook = maxList.First()
End Sub

Sub UseTwoQueries()
	testBooks = GetBooksForPerformance()

	Run( _
	  runCount, _
	  Nothing, _
	  New Action(AddressOf TestMaxElementWithTwoQueries) _
	)
End Sub

Private Sub TestMaxElementWithCustomOperator()
	Dim maxBook = testBooks.MaxElement(Function(book) book.PageCount)
End Sub

Sub UseMaxElement()
	testBooks = GetBooksForPerformance()

	Run( _
	  runCount, _
	  Nothing, _
	  New Action(AddressOf TestMaxElementWithCustomOperator) _
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

End Class ' Temporary hack to enable extension methods

Module Extensions

  <System.Runtime.CompilerServices.Extension()> _
  Public Function MaxElement(Of TElement, TData As IComparable(Of TData))( _
	  ByVal source As IEnumerable(Of TElement), _
	  ByVal selector As Func(Of TElement, TData)) As TElement
	If source Is Nothing Then
	  Throw New ArgumentNullException("source")
	End If
	If selector Is Nothing Then
	  Throw New ArgumentNullException("selector")
	End If

	Dim firstElement As Boolean = True
	Dim result As TElement ' = default(TElement)
	Dim maxValue As TData ' = default(TData)
	For Each element In source
	  Dim candidate = selector(element)
	  If firstElement Or (candidate.CompareTo(maxValue) > 0) Then
		firstElement = False
		maxValue = candidate
		result = element
	  End If
	Next
	Return result
  End Function
  
End Module

Class Hack ' Temporary hack to enable extension methods