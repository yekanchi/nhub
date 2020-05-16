<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Enum TestEnum
	FilterOnInt
	FilterOnString
	Sort
	Join
End Enum

' (1) Set runCount and test to custom values to change the tests
Const runCount As Integer = 2
Const test As TestEnum = TestEnum.FilterOnInt

Private results As List(Of Book)
Private testBooks As List(Of Book)

Sub Main()
  ' (2) Uncomment the test you want to run

'  Console.WriteLine("foreach:")
'  UseForeach()

'  Console.WriteLine("for:")
'  UseFor()

'  Console.WriteLine("List<T>.FindAll")
'  UseListFindAll()

'  Console.WriteLine("List<T>.ForEach")
'  UseListForEach()

'  Console.WriteLine("LINQ with ToList")
'  UseLinqWithToList()

'  Console.WriteLine("LINQ with presized list")
'  UseLinqWithPresizedList()
End Sub

Private Sub ClearResults()
	results.Clear()
End Sub

#Region "For Each"

  Private Sub TestPerformance_ForEach_FilterOnInt()
	For Each book In testBooks
	  If book.PageCount > 500 Then
		results.Add(book)
	  End If
	Next
  End Sub
  Private Sub TestPerformance_ForEach_FilterOnString()
	For Each book In testBooks
	  If book.Title.StartsWith("1") Then
		results.Add(book)
	  End If
	Next
  End Sub
  Private Sub TestPerformance_ForEach_Sort()
	For Each book In testBooks
	  If book.PageCount > 500 Then
		results.Add(book)
	  End If
	Next
	results.Sort(Function(book1, book2) book1.Title.CompareTo(book2.Title))
  End Sub
  Private Sub TestPerformance_ForEach_Join()
	For Each publisher In SampleData.Publishers
	  For Each book In testBooks
		If publisher.ReferenceEquals(book.Publisher, publisher) And book.PageCount > 500 Then
		  results.Add(book)
		End If
	  Next
	Next
  End Sub

Sub UseForeach()
	testBooks = GetBooksForPerformance()

	If test = TestEnum.FilterOnInt
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_ForEach_FilterOnInt) _
		)
	ElseIf test = TestEnum.FilterOnString
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_ForEach_FilterOnString) _
		)
	ElseIf test = TestEnum.Sort
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_ForEach_Sort) _
		)
	ElseIf test = TestEnum.Join
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_ForEach_Join) _
		)
	Else
		Throw New NotImplementedException()
	End If
End Sub

#End Region

#Region "For"

  Private Sub TestPerformance_For_FilterOnInt()
	For i = 0 To testBooks.Count - 1
	  Dim book = testBooks(i)
	  If book.PageCount > 500 Then
		results.Add(book)
	  End If
	Next
  End Sub
  Private Sub TestPerformance_For_FilterOnString()
	For i = 0 To testBooks.Count - 1
	  Dim book = testBooks(i)
	  If book.Title.StartsWith("1") Then
		results.Add(book)
	  End If
	Next
  End Sub

Sub UseFor()
	testBooks = GetBooksForPerformance()

	If test = TestEnum.FilterOnInt
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_For_FilterOnInt) _
		)
	ElseIf test = TestEnum.FilterOnString
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_For_FilterOnString) _
		)
	Else
		Throw New NotImplementedException()
	End If
End Sub

#End Region

#Region "List(Of T).FindAll"

  Private Sub TestPerformance_ListFindAll_FilterOnInt()
	results = testBooks.FindAll(Function(book) book.PageCount > 500)
  End Sub
  Private Sub TestPerformance_ListFindAll_FilterOnString()
	results = testBooks.FindAll(Function(book) book.Title.StartsWith("1"))
  End Sub

' Does not return titles
' Does not pre-size the list
Sub UseListFindAll()
	testBooks = GetBooksForPerformance()

	If test = TestEnum.FilterOnInt
		Run( _
		  	runCount, _
			Nothing, _
			New Action(AddressOf TestPerformance_ListFindAll_FilterOnInt) _
		)
	ElseIf test = TestEnum.FilterOnString
		Run( _
		  	runCount, _
			Nothing, _
			New Action(AddressOf TestPerformance_ListFindAll_FilterOnString) _
		)
	Else
		Throw New NotImplementedException()
	End If
End Sub

#End Region

#Region "List(Of T).ForEach"

  Private Sub ListForEach_FilterOnInt(ByVal book As Book)
	If book.PageCount > 500 Then
	  results.Add(book)
	End If
  End Sub
  Private Sub ListForEach_FilterOnString(ByVal book As Book)
	If book.Title.StartsWith("1") Then
	  results.Add(book)
	End If
  End Sub

  Private Sub TestPerformance_ListForEach_FilterOnInt()
	testBooks.ForEach(AddressOf ListForEach_FilterOnInt)
  End Sub
  Private Sub TestPerformance_ListForEach_FilterOnString()
	testBooks.ForEach(AddressOf ListForEach_FilterOnString)
  End Sub

Sub UseListForEach()
	testBooks = GetBooksForPerformance()

	If test = TestEnum.FilterOnInt
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_ListForEach_FilterOnInt) _
		)
	ElseIf test = TestEnum.FilterOnString
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_ListForEach_FilterOnString) _
		)
	Else
		Throw New NotImplementedException()
	End If
End Sub

#End Region

#Region "LINQ with ToList"

  Private Sub TestPerformance_ToList_FilterOnInt()
	Dim query = _
	  From book In testBooks _
	  Where book.PageCount > 500 _
	  Select book
	query.ToList()
  End Sub
  Private Sub TestPerformance_ToList_FilterOnString()
	Dim query = _
	  From book In testBooks _
	  Where book.Title.StartsWith("1") _
	  Select book
	query.ToList()
  End Sub

' Does not pre-size the list
Sub UseLinqWithToList()
	testBooks = GetBooksForPerformance()

	If test = TestEnum.FilterOnInt
		Run( _
		  	runCount, _
			Nothing, _
			New Action(AddressOf TestPerformance_ToList_FilterOnInt) _
		)
	ElseIf test = TestEnum.FilterOnString
		Run( _
		  	runCount, _
			Nothing, _
			New Action(AddressOf TestPerformance_ToList_FilterOnString) _
		)
	Else
		Throw New NotImplementedException()
	End If
End Sub

#End Region

#Region "LINQ with presized list"

  Private Sub TestPerformance_LinqPresized_FilterOnInt()
	Dim query = _
	  From book In testBooks _
	  Where book.PageCount > 500 _
	  Select book
	For Each book In query
	  results.Add(book)
	Next
  End Sub
  Private Sub TestPerformance_LinqPresized_FilterOnString()
	Dim query = _
	  From book In testBooks _
	  Where book.Title.StartsWith("1") _
	  Select book
	For Each book In query
	  results.Add(book)
	Next
  End Sub
  Private Sub TestPerformance_LinqPresized_Sort()
	Dim query = _
	  From book In testBooks _
	  Where book.PageCount > 500 _
	  Order By book.Title _
	  Select book
	For Each book In query
	  results.Add(book)
	Next
  End Sub
  Private Sub TestPerformance_LinqPresized_Join()
	Dim query = _
	  From publisher In SampleData.Publishers _
	  Join book In testBooks On publisher Equals book.Publisher _
	  Where book.PageCount > 500 _
	  Select book
	For Each book In query
	  results.Add(book)
	Next
  End Sub

Sub UseLinqWithPresizedList()
	testBooks = GetBooksForPerformance()

	If test = TestEnum.FilterOnInt
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_LinqPresized_FilterOnInt) _
		)
	ElseIf test = TestEnum.FilterOnString
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_LinqPresized_FilterOnString) _
		)
	ElseIf test = TestEnum.Sort
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_LinqPresized_Sort) _
		)
	ElseIf test = TestEnum.Join
		results = New List(Of Book)(499357)
		Run( _
		  	runCount, _
			New Action(AddressOf ClearResults), _
			New Action(AddressOf TestPerformance_LinqPresized_Join) _
		)
	Else
		Throw New NotImplementedException()
	End If
End Sub

#End Region

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