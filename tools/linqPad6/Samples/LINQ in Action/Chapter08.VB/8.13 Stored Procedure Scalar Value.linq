<Query Kind="VBProgram">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
     <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Sub Main
	Dim publisherId = New System.Guid("851e3294-145d-4fff-a190-3cab7aa95f76")
			   
	Console.WriteLine(String.Format("Books found: {0}", _
		BookCountForPublisher(publisherId).ToString()))
End Sub

' Define other methods and classes here
<System.Data.Linq.Mapping.Function(Name:="dbo.BookCountForPublisher")> _
Public Function BookCountForPublisher( _
	<Parameter(Name:="@PublisherId")> ByVal PublisherId As Guid) As Integer

	Dim result As IExecuteResult = Me.ExecuteMethodCall( _
					Me, DirectCast(MethodInfo.GetCurrentMethod(), MethodInfo), PublisherId)
	Return CInt(result.ReturnValue)
End Function