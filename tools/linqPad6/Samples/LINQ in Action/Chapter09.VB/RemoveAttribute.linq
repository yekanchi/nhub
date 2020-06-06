<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim book As XElement = New XElement("book", New XAttribute("pubDate", "July 31, 2006"))
book.Attribute("pubDate").Remove()

Console.WriteLine(book)