<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim books As XElement = <books>
						  <book>
							<title>LINQ in Action</title>
							<author>Steve Eichert</author>
						  </book>
						</books>

books.Element("book").SetElementValue("author", New XElement("foo"))