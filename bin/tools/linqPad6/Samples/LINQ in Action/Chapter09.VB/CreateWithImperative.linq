<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim book As XElement = New XElement("book")
book.Add(New XElement("author", "Don Box"))
book.Add(New XElement("title", "Essential .NET"))

Dim books As XElement = New XElement("books")
books.Add(book)

Console.WriteLine(books)