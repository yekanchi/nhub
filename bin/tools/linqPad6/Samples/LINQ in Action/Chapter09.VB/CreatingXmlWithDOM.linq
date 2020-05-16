<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

 Dim doc As XmlDocument = New XmlDocument()
	Dim books As XmlElement = doc.CreateElement("books")
	Dim author1 As XmlElement = doc.CreateElement("author")
	author1.InnerText = "Fabrice Marguerie"
	Dim author2 As XmlElement = doc.CreateElement("author")
	author2.InnerText = "Steve Eichert"
	Dim author3 As XmlElement = doc.CreateElement("author")
	author3.InnerText = "Jim Wooley"
	Dim title As XmlElement = doc.CreateElement("title")
	title.InnerText = "LINQ in Action"
	Dim book As XmlElement = doc.CreateElement("book")
	book.AppendChild(author1)
	book.AppendChild(author2)
	book.AppendChild(author3)
	book.AppendChild(title)
	books.AppendChild(book)
	doc.AppendChild(books)
	
Console.WriteLine(doc.OuterXml)