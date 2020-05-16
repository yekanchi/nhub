<Query Kind="Expression">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

new XElement("books",
				new XElement("book",
				  new XElement("title", "LINQ in Action"),
				  new XElement("authors",
					new XElement("author", "Fabrice Marguerie"),
					new XElement("author", "Steve Eichert"),
					new XElement("author", "Jim Wooley")
				  ),
				  new XElement("publicationDate", "June 2007")
				),
				new XElement("book",
				  new XElement("title", "Ajax in Action"),
				  new XElement("authors",
					new XElement("author", "Dave Crane"),
					new XElement("author", "Eric Pascarello"),
					new XElement("author", "Darren James")
				  ),
				  new XElement("publicationDate", "October 2005")
				)
			  )