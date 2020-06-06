<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim bookXml As XElement = <books>
							  <book>
								<title>LINQ in Action</title>
								<author>Steve Eichert</author>
							  </book>
							</books>
bookXml.Element("book").SetElementValue("author", "Bill Gates")
Console.WriteLine(bookXml)