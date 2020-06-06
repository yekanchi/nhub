<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Sub Main()
	Dim csv = _
		"#Books (format: ISBN, Title, Authors, Publisher, Date, Price)" + Environment.NewLine + _
		"0735621632,CLR via C#,Jeffrey Richter,Microsoft Press,02-22-2006,59.99"  + Environment.NewLine + _
		"0321127420,Patterns Of Enterprise Application Architecture,Martin Fowler,Addison-Wesley, 11-05-2002,54.99" + Environment.NewLine + _
		"0321200683,Enterprise Integration Patterns,Gregor Hohpe,Addison-Wesley,10-10-2003,54.99" + Environment.NewLine + _
		"0321125215,Domain-Driven Design,Eric Evans,Addison-Wesley Professional,08-22-2003,54.99" + Environment.NewLine + _
		"1932394613,Ajax In Action,Dave Crane;Eric Pascarello;Darren James,Manning Publications,10-01-2005,44.95"
	
	Using reader = New StringReader(csv)
	  Dim books = _
		From line In reader.Lines().Reverse() _
		Where Not line.StartsWith("#") _
		Let parts = line.Split(",") _
		Select New With {.Isbn = parts(0), .Title = parts(1), .Publisher = parts(3)}
	  ' Reverse breaks streaming because it processes all lines at once

	  books.Dump()
	End Using
	' Warning, the reader should not be disposed while we are likely to enumerate the query!
	' Don't forget that deferred execution happens here
End Sub

End Class ' Temporary hack to enable extension methods

''' <summary>
''' Operators for LINQ to Text Files
''' </summary>
Module Extensions

  <System.Runtime.CompilerServices.Extension()> _
  Public Function Lines(ByVal source As TextReader) As IEnumerable(Of String)
	If source Is Nothing Then
	  Throw New ArgumentNullException("source")
	End If

	Return New TextReaderEnumerable(source)
  End Function

#Region "TextReaderEnumerator"

  Class TextReaderEnumerator
	Implements IEnumerator(Of String)

	Private currentLine As String
	Private source As TextReader

	Public Sub New(ByVal source As TextReader)
	  Me.source = source
	End Sub

	Public ReadOnly Property Current() As String _
	  Implements System.Collections.Generic.IEnumerator(Of String).Current
	  Get
		Return Me.currentLine
	  End Get
	End Property

	Public ReadOnly Property CurrentNonGeneric() As Object _
	  Implements System.Collections.IEnumerator.Current
	  Get
		Return Current
	  End Get
	End Property

	Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
	  currentLine = source.ReadLine()
	  Return Not currentLine Is Nothing
	End Function

	Public Sub Reset() Implements System.Collections.IEnumerator.Reset

	End Sub

	Private disposedValue As Boolean = False    ' To detect redundant calls

	' IDisposable
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
	  If Not Me.disposedValue Then
		If disposing Then
		  ' TODO: free other state (managed objects).
		End If

		' TODO: free your own state (unmanaged objects).
		' TODO: set large fields to null.
	  End If
	  Me.disposedValue = True
	End Sub

#Region " IDisposable Support "
	' This code added by Visual Basic to correctly implement the disposable pattern.
	Public Sub Dispose() Implements IDisposable.Dispose
	  ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
	  Dispose(True)
	  GC.SuppressFinalize(Me)
	End Sub
#End Region

  End Class

#End Region

#Region "TextReaderEnumerable"

  Class TextReaderEnumerable
	Implements IEnumerable(Of String)

	Private source As TextReader
	Public Sub New(ByVal source As TextReader)
	  Me.source = source
	End Sub
	Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of String) _
	  Implements System.Collections.Generic.IEnumerable(Of String).GetEnumerator
	  Return New TextReaderEnumerator(source)
	End Function
	Public Function GetEnumeratorNonGeneric() As System.Collections.IEnumerator _
	  Implements System.Collections.IEnumerable.GetEnumerator
	  Return GetEnumerator()
	End Function
  End Class

#End Region

End Module

Class Hack ' Temporary hack to enable extension methods