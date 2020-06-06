<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement book = new XElement("book");
book.Add(new XAttribute("pubDate", "July 31, 2006"));
Console.WriteLine(book);