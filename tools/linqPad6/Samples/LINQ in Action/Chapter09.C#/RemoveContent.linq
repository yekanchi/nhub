<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement books = XElement.Parse(
@"<books>
	<book>
		<author>Foo Man Choo</author>
	</book>
</books>");

books.Element("book").Element("author").Value = String.Empty;
Console.WriteLine(books);