<Query Kind="VBExpression">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

New XElement("books", _
	 New XElement("book", _
		New XElement("title", "LINQ in Action"), _
		New XElement("authors", _
			New XElement("author", "Fabrice Marguerie"), _
			New XElement("author", "Steve Eichert"), _
			New XElement("author", "Jim Wooley") _
			), _
		New XElement("publicationDate", "June 2007") _
		), _
	 New XElement("book", _
		New XElement("title", "Ajax in Action"), _
		New XElement("authors", _
			New XElement("author", "Dave Crane"), _
			New XElement("author", "Eric Pascarello"), _
			New XElement("author", "Darren James") _
			), _
		New XElement("publicationDate", "October 2005") _
		) _
	 )