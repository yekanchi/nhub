<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim books As XElement = New XElement("books")
books.Add(New XElement("book", _
	New XAttribute("publicationDate", "May 2006"), _
	New XElement("author", "Chris Sells"), _
	New XElement("title", "Windows Forms Programming") _
	) _
)

Console.WriteLine(books)