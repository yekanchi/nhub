<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement book = new XElement("book", new XAttribute("pubDate", "July 31, 2006"));
book.SetAttributeValue("pubDate", "October 1, 2006");

Console.WriteLine(book);