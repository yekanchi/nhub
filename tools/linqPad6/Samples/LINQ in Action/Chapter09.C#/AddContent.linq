<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement book = new XElement("book");
book.Add(new XElement("author", "Dr. Seuss"));
Console.WriteLine(book);