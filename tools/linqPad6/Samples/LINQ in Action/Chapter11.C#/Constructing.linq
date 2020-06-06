<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";

XmlDocument doc = new XmlDocument();
XmlElement books = doc.CreateElement("books");
XmlElement firstBook = doc.CreateElement("book");
XmlElement title = doc.CreateElement("title");
title.InnerText = "Linq in Action";

XmlElement author1 = doc.CreateElement("author");
author1.InnerText = "Fabrice Marguerie";

XmlElement author2 = doc.CreateElement("author");
author2.InnerText = "Steve Eichert";

XmlElement publisher = doc.CreateElement("publisher");
publisher.InnerText = "Manning";

firstBook.AppendChild(title);
firstBook.AppendChild(author1);
firstBook.AppendChild(author2);
firstBook.AppendChild(publisher);

books.AppendChild(firstBook);
doc.AppendChild(books);
doc.Save(Path + "book-dom.xml");


XElement xml = new XElement("books",
new XElement("book",
	new XElement("title", "Linq in Action"),
	new XElement("author", "Fabrice Marguerie"),
	new XElement("author", "Steve Eichert"),
	new XElement("publisher", "Manning"),
				new XElement("rating", "5")
),
new XElement("book",
	new XElement("title", "Ajax in Action"),
	new XElement("author", "Foo"),
	new XElement("publisher", "Manning"),
	new XElement("rating", "3")
),
new XElement("book",
	new XElement("title", "Enterprise Application Architecture"),
	new XElement("author", "Martin Fowler"),
	new XElement("publisher", "APress"),
	new XElement("rating", "5")
)
);

Console.WriteLine(xml);