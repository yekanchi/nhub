<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement book = new XElement("book", new XAttribute("pubDate", "July 31, 2006"));
book.Attribute("pubDate").Remove();

Console.WriteLine(book);