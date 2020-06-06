<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement books = XElement.Parse(
@"<books>
	<book>
		<title>LINQ in Action</title>
		<author>Steve Eichert</author>
	</book>
</books>");

books.Element("book").ReplaceNodes(
		new XElement("title", "Ajax in Action"),
		new XElement("author", "Dave Crane")
	);

Console.WriteLine(books);