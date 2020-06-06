<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim books As XElement = <books>
						  <book>
							<title>LINQ in Action</title>
							<author>Steve Eichert</author>
						  </book>
						</books>

books.Element("book").ReplaceNodes( _
 New XElement("title", "Ajax in Action"), _
 New XElement("author", "Dave Crane") _
)

Console.WriteLine(books)