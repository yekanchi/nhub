<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XmlDocument doc = new XmlDocument();
XmlElement books = doc.CreateElement("books");
XmlElement author1 = doc.CreateElement("author");
author1.InnerText = "Fabrice Marguerie";
XmlElement author2 = doc.CreateElement("author");
author2.InnerText = "Steve Eichert";
XmlElement author3 = doc.CreateElement("author");
author3.InnerText = "Jim Wooley";
XmlElement title = doc.CreateElement("title");
title.InnerText = "LINQ in Action";
XmlElement book = doc.CreateElement("book");
book.AppendChild(author1);
book.AppendChild(author2);
book.AppendChild(author3);
book.AppendChild(title);
books.AppendChild(book);
doc.AppendChild(books);

Console.WriteLine(doc.OuterXml);