<Query Kind="VBStatements">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.Chapter06to08.Common.dll</Reference>
  <Namespace>System.Data.Linq.Mapping</Namespace>
  <Namespace>LinqInAction.Chapter06to08.Common</Namespace>
</Query>

Dim context As New LinqBooksDataContext(Connection.ConnectionString)
Dim bookParam = Expression.Parameter(GetType(LinqInAction.Chapter06to08.Common.Book), "book")
Dim query = _
  context.Books.Where(Expression.Lambda(Of Func(Of LinqInAction.Chapter06to08.Common.Book, Boolean)) _
					   (Expression.GreaterThan( _
							Expression.Property( _
								bookParam, _
								GetType(LinqInAction.Chapter06to08.Common.Book).GetProperty("Price")), _
							Expression.Constant(30D, GetType(Decimal?))), _
						New ParameterExpression() {bookParam}))

query.Dump()