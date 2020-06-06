<Query Kind="VBProgram">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

'6.2 Select book title and price for books less than $35
Sub Main
	Dim books As IEnumerable(Of Book) = Book.GetBooks().Dump()	
	Dim query = From _book In books _
				Where _book.Price < 35 _
				Order By _book.Title _
				Select _book.Title, _book.Price
				
	query.Dump()
End Sub

' Define other methods and classes here
  Public Class Book

		Private _BookId As Guid
		Public Property BookId() As Guid
			Get
				Return _BookId
			End Get
			Set(ByVal value As Guid)
				_BookId = value
			End Set
		End Property


		Private _Isbn As String
		Public Property Isbn() As String
			Get
				Return _Isbn
			End Get
			Set(ByVal value As String)
				_Isbn = value
			End Set
		End Property

		Private _Notes As String
		Public Property Notes() As String
			Get
				Return _Notes
			End Get
			Set(ByVal value As String)
				_Notes = value
			End Set
		End Property

		Private _PageCount As Integer
		Public Property PageCount() As Integer
			Get
				Return _PageCount
			End Get
			Set(ByVal value As Integer)
				_PageCount = value
			End Set
		End Property

		Private _Price As Decimal
		Public Property Price() As Decimal
			Get
				Return _Price
			End Get
			Set(ByVal value As Decimal)
				_Price = value
			End Set
		End Property

		Private _PublicationDate As DateTime
		Public Property PublicationDate() As DateTime
			Get
				Return _PublicationDate
			End Get
			Set(ByVal value As DateTime)
				_PublicationDate = value
			End Set
		End Property

		Private _Summary As String
		Public Property Summary() As String
			Get
				Return _Summary
			End Get
			Set(ByVal value As String)
				_Summary = value
			End Set
		End Property

		Private _Title As String
		Public Property Title() As String
			Get
				Return _Title
			End Get
			Set(ByVal value As String)
				_Title = value
			End Set
		End Property

		Private _SubjectId As Guid
		Public Property SubjectId() As Guid
			Get
				Return _SubjectId
			End Get
			Set(ByVal value As Guid)
				_SubjectId = value
			End Set
		End Property

		Private _PublisherId As Guid
		Public Property PublisherId() As Guid
			Get
				Return _PublisherId
			End Get
			Set(ByVal value As Guid)
				_PublisherId = value
			End Set
		End Property

		Public Shared Function GetBooks() As IEnumerable(Of Book)
			Dim books As New List(Of Book)
			Using connection As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\lia.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True")
				connection.Open()
				Using command = connection.CreateCommand()
					command.CommandText = "SELECT ID, Isbn, Notes, PageCount, Price, PubDate, Publisher, Subject, Summary, Title FROM dbo.Book"
					Using reader As SqlDataReader = command.ExecuteReader()
						While (reader.Read())
							Dim newBook As New Book()
							If Not reader.IsDBNull(0) Then newBook.BookId = reader.GetGuid(0)
							If Not reader.IsDBNull(1) Then newBook.Isbn = reader.GetString(1)
							If Not reader.IsDBNull(2) Then newBook.Notes = reader.GetString(2)
							If Not reader.IsDBNull(3) Then newBook.PageCount = reader.GetInt32(3)
							If Not reader.IsDBNull(4) Then newBook.Price = reader.GetDecimal(4)
							If Not reader.IsDBNull(5) Then newBook.PublicationDate = reader.GetDateTime(5)
							If Not reader.IsDBNull(6) Then newBook.PublisherId = reader.GetGuid(6)
							If Not reader.IsDBNull(7) Then newBook.SubjectId = reader.GetGuid(7)
							If Not reader.IsDBNull(8) Then newBook.Summary = reader.GetString(8)
							If Not reader.IsDBNull(9) Then newBook.Title = reader.GetString(9)
							books.Add(newBook)
						End While
					End Using
				End Using
			End Using
			Return books
		End Function
	End Class