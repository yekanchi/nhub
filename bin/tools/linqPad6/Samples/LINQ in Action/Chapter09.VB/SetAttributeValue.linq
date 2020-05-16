<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim book As XElement = New XElement("book", New XAttribute("pubDate", "July 31, 2006"))
book.SetAttributeValue("pubDate", "October 1, 2006")

Console.WriteLine(book)