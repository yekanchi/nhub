<Query Kind="Statements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

//The code below dynamically creates a query at run-time that is equivalent to the following query:
//var query =
//  from book in SampleData.Books
//  where (book.Title != "Funny Stories") && (book.PageCount > 100)
//  select book;

// define the book variable
var book = Expression.Parameter(typeof(Book), "book");
// book.Title != "Funny Stories"
var titleExpression = Expression.NotEqual(
  Expression.Property(book, "Title"), Expression.Constant("Funny Stories"));
// book.PageCount > 100
var pageCountExpression = Expression.GreaterThan(
  Expression.Property(book, "PageCount"), Expression.Constant(100));
// and
var andExpression = Expression.And(titleExpression, pageCountExpression);
// create the where clause
var predicate = Expression.Lambda(andExpression, book);
var query = Enumerable.Where(SampleData.Books, (Func<Book, Boolean>)predicate.Compile());
  
query.Dump();