<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"

Dim xml As XElement = _
	 New XElement("html", _
	 New XElement("body", _
	 New XElement("h1", "LINQ Books Library"), _
	 New XElement("div", _
	 New XElement("b", "LINQ in Action"), _
	 "\n" + _
	 "      By: Fabrice Marguerie, Steve Eichert\n" + _
	 "      Published By: Manning\n" + _
	 "    " _
	 ), _
	 New XElement("div", _
	 New XElement("b", "AJAX in Action"), _
	 "\n" + _
	 "      By: Dave Crane\n" + _
	 "      Published By: Manning\n" + _
	 "    " _
	 ), _
	 New XElement("div", _
	 New XElement("b", "Patterns of Enterprise Application Architecture"), _
	 "\n" + _
	 "      By: Martin Fowler\n" + _
	 "      Published By: APress\n" + _
	 "    " _
	 ) _
	 ) _
	)

Dim booksXml As XElement = XElement.Load(Path & "books.xml.sdf")
Dim books = From book In booksXml.Descendants("book") _
	 Select New With { _
	 .Title = CType(book.Element("title"), String), _
	 .Publisher = CType(book.Element("publisher"), String), _
	 .Authors = String.Join(", ", book.Descendants("author").Select(Function(b) CType(b, String)).ToArray()) _
	 }

For Each book In books
  Console.WriteLine(book.Title)
  Console.WriteLine(book.Publisher)
  Console.WriteLine(book.Authors)
Next

Dim html As XElement = _
	 New XElement("html", _
	 New XElement("body", _
	 New XElement("h1", "LINQ Books Library"), _
	 From book In booksXml.Descendants("book") _
	 Select New XElement("div", _
	 New XElement("b", CType(book.Element("title"), String), _
	 "<br />" + _
	 "      By: " + String.Join(", ", book.Descendants("author").Select(Function(b) CType(b, String)).ToArray()) + "<br />" + _
	 "      Published By: " + CType(book.Element("publisher"), String) + "<br />" + _
	 "    " _
	 ) _
	 ) _
	 ) _
	 )

Console.WriteLine(html)