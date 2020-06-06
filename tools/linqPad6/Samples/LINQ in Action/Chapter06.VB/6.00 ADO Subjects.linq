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

'6.0 ADO - Subjects
Sub Main
	Subject.GetSubjects().Dump()
End Sub

' Define other methods and classes here
	Public Class Subject

		Private _SubjectId As Guid
		Public Property SubjectId() As Guid
			Get
				Return _SubjectId
			End Get
			Set(ByVal value As Guid)
				_SubjectId = value
			End Set
		End Property


		Private _Description As String
		Public Property Description() As String
			Get
				Return _Description
			End Get
			Set(ByVal value As String)
				_Description = value
			End Set
		End Property


		Private _Name As String
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				_Name = value
			End Set
		End Property

		Public Shared Function GetSubjects() As IEnumerable(Of Subject)
			Dim subjects As New List(Of Subject)
			Using connection As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\lia.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True")
				connection.Open()
				Using command As SqlCommand = connection.CreateCommand()
					command.CommandText = "SELECT ID, Name, Description FROM dbo.Subject"
					Using reader As SqlDataReader = command.ExecuteReader()
						While (reader.Read())
							Dim newSubject As New Subject()
							If Not reader.IsDBNull(0) Then newSubject.SubjectId = reader.GetGuid(0)
							If Not reader.IsDBNull(1) Then newSubject.Description = reader.GetString(1)
							If Not reader.IsDBNull(2) Then newSubject.Name = reader.GetString(2)
							subjects.Add(newSubject)
						End While
					End Using
				End Using
			End Using
			Return subjects
		End Function
	End Class