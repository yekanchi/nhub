<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim book As XElement = New XElement("book")
book.Add(New XElement("author", "Dr. Seuss"))
Console.WriteLine(book)