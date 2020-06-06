<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement xml =
		  new XElement("html",
			  new XElement("body",
					  new XElement("h1", "LINQ Books Library"),
					  new XElement("div",
							  new XElement("b", "LINQ in Action"),
							  "\n" +
							  "      By: Fabrice Marguerie, Steve Eichert\n" +
							  "      Published By: Manning\n" +
							  "    "
					  ),
					  new XElement("div",
							  new XElement("b", "AJAX in Action"),
							  "\n" +
							  "      By: Dave Crane\n" +
							  "      Published By: Manning\n" +
							  "    "
					  ),
					  new XElement("div",
							  new XElement("b", "Patterns of Enterprise Application Architecture"),
							  "\n" +
							  "      By: Martin Fowler\n" +
							  "      Published By: APress\n" +
							  "    "
					  )
			  )
	  );

	  string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";

	  XElement booksXml = XElement.Load(Path + "books.xml.sdf");
	  var books = from book in booksXml.Descendants("book")
				  select new
				  {
					Title = (string)book.Element("title"),
					Publisher = (string)book.Element("publisher"),
					Authors = String.Join(", ", book.Descendants("author").Select(b => (string)b).ToArray())
				  };

	  foreach (var book in books)
	  {
		Debug.WriteLine(book.Title);
		Debug.WriteLine(book.Publisher);
		Debug.WriteLine(book.Authors);
	  }

	  XElement html =
		  new XElement("html",
			  new XElement("body",
					  new XElement("h1", "LINQ Books Library"),
					  from book in booksXml.Descendants("book")
					  select new XElement("div",
						  new XElement("b", (string)book.Element("title")),
						  "\n" +
						  "      By: " + String.Join(", ", book.Descendants("author").Select(b => (string)b).ToArray()) + "\n" +
						  "      Published By: " + (string)book.Element("publisher") + "\n" +
						  "    "
					  )
			  )
	  );

	  Console.WriteLine(html);