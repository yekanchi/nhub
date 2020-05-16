<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

'The code below dynamically creates a query at run-time that is equivalent to the following query:
'Dim query = _
'  From book In SampleData.Books _
'  Where (book.Title <> "Funny Stories") And (book.PageCount > 100) _
'  Select book

' define the book variable
Dim book = Expression.Parameter(GetType(Book), "book")
' book.Title <> "Funny Stories"
Dim titleExpression = Expression.NotEqual( _
  Expression.Property(book, "Title"), Expression.Constant("Funny Stories"))
' book.PageCount > 100
Dim pageCountExpression = Expression.GreaterThan( _
  Expression.Property(book, "PageCount"), Expression.Constant(100))
' and
Dim andExpression = Expression.And(titleExpression, pageCountExpression)
' create the where clause
Dim predicate = Expression.Lambda(andExpression, book)
Dim query = Enumerable.Where(SampleData.Books, CType(predicate.Compile(), Func(Of Book, Boolean)))

query.Dump()