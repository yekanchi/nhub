<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Dim root As XElement = XElement.Load(Path & "categorizedBooks.xml.sdf")
Dim dddBook As XElement = root.Descendants("book") _
 .Where(Function(book As XElement) CType(book, String) = "Domain Driven Design") _
 .First()

Dim beforeSelf As IEnumerable(Of XElement) = dddBook.ElementsBeforeSelf()
beforeSelf.Dump()