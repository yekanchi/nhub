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

books.Element("book").Element("author").ReplaceNodes(new XElement("foo"));

Console.WriteLine(books);