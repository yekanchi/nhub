<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Console.WriteLine(
		new XElement("books",
		  new XElement("book",
			new XElement("author", "Fabrice Marguerie"),
			new XElement("author", "Steve Eichert"),
			new XElement("author", "Jim Wooley"),
			new XElement("title", "LINQ in Action"),
			new XElement("publisher", "Manning")
		  )
		)
	);