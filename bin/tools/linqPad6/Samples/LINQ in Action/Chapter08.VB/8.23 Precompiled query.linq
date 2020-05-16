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

Sub Main
	ExpensiveBooks(me, 35).Dump()
End Sub

''' <summary>
''' Precompiled version of the Expensive Books query
''' </summary>
Public Shared ExpensiveBooks As Func(Of TypedDataContext, Decimal, IQueryable(Of Book)) = _
	CompiledQuery.Compile(Function(context As TypedDataContext, minimumPrice As Decimal) _
	From book In context.Books() _
	Where (book.Price >= minimumPrice) _
			Select book)