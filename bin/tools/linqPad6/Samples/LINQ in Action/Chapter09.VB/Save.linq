<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim books As XElement = New XElement("books", _
	New XElement("book", _
	New XElement("title", "LINQ in Action"), _
	New XElement("author", "Steve Eichert") _
	) _
)
books.Save("saved-books.XML", SaveOptions.DisableFormatting)