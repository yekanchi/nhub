<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim books As XElement = <books>
						  <book>
							<author>Foo Man Choo</author>
						  </book>
						</books>


books.Element("book").Element("author").Value = String.Empty
Console.WriteLine(books)