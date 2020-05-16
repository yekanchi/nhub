<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement book = new XElement("book");
book.Add(new XAttribute("publicationDate", "October 2005"));
Console.WriteLine(book);