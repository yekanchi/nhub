<Query Kind="Statements">
  <Connection>
    <ID>65e0892b-2c67-48c8-9d4f-3aa69a59de16</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia6-8.mdf</AttachFileName>
  </Connection>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.Chapter06to08.Common.dll</Reference>
  <Namespace>LinqInAction.Chapter06to08.Common</Namespace>
</Query>

LinqBooksDataContext context = new LinqBooksDataContext(this.Connection.ConnectionString);
var bookParam = Expression.Parameter(typeof(LinqInAction.Chapter06to08.Common.Book), "book");

	var query =
		context.Books.Where<LinqInAction.Chapter06to08.Common.Book>(Expression.Lambda<Func<LinqInAction.Chapter06to08.Common.Book, bool>>
			(Expression.GreaterThan(
				Expression.Property(
					bookParam,
					typeof(LinqInAction.Chapter06to08.Common.Book).GetProperty("Price")),
				Expression.Constant(30M, typeof(decimal?))),
			new ParameterExpression[] { bookParam }));

query.Dump();