<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement books = new XElement("books",
		new XElement("book",
		new XElement("title", "LINQ in Action"),
		new XElement("author", "Fabrice Marguerie"),
		new XElement("author", "Steve Eichert"),
		new XElement("author", "Jim Wooley")
		)
	);
books.Save(@"books.XML", SaveOptions.DisableFormatting);