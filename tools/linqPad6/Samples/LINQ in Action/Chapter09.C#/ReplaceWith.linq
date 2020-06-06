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

var titles = books.Descendants("title").ToList();
foreach (XElement title in titles)
{
	title.ReplaceWith(new XElement("book_title", (string)title));
}

Console.WriteLine(books);