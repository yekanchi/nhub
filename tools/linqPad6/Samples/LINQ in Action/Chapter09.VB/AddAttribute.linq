<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim book As XElement = New XElement("book")
book.Add(New XAttribute("publicationDate", "October 2005"))
Console.WriteLine(book)