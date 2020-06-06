<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string bookXml =
@"<books>
	<book>
		<title>LINQ in Action</title>
		<author>Steve Eichert</author>
	</book>
</books>";
XElement books = XElement.Parse(bookXml);
books.Element("book").SetElementValue("author", "Bill Gates");
Console.WriteLine(books);