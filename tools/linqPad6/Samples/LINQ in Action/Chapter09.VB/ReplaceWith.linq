<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim books As XElement = <books>
						  <book>
							<title>LINQ in Action</title>
							<author>Steve Eichert</author>
						  </book>
						</books>


Dim titles = books.Descendants("title").ToList()
For Each title As XElement In titles
  title.ReplaceWith(New XElement("book_title", CType(title, String)))
Next

Console.WriteLine(books)