<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement book = new XElement("book");
book.Add(new XElement("author", "Don Box"));
book.Add(new XElement("title", "Essential .NET"));

XElement books = new XElement("books");
books.Add(book);

Console.WriteLine(books);