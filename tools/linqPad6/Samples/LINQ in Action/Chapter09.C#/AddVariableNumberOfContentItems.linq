<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement books = new XElement("books");
books.Add(new XElement("book",
		  new XAttribute("publicationDate", "May 2006"),
		  new XElement("author", "Chris Sells"),
		  new XElement("title", "Windows Forms Programming")
		)
);

Console.WriteLine(books);