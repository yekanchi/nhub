<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Dim root As XElement = XElement.Load(Path & "categorizedBooks.xml.sdf")
Dim books = From book In root.Descendants("book") _
 Select CType(book, String)

books.Dump()