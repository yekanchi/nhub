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

books.Element("book").SetElementValue("author", "Bill Gates");

Console.WriteLine(books);