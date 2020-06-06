<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Dim existingBooks As XElement = XElement.Load(Path & "existingBooks.xml.sdf")
Dim books As XElement = New XElement("books")
books.Add(existingBooks.Elements("book"))

books.Dump("Before Adding")

Dim newBook As XElement = New XElement("book", "LINQ in Action")
Dim firstBook As XElement = books.Element("book")
firstBook.AddAfterSelf(newBook)

books.Dump("After Adding 'LINQ in Action'")