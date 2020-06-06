<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Dim root As XElement = XElement.Load(Path & "categorizedBooks.xml.sdf")
Dim dddBook As XElement = root.Descendants("book") _
 .Where(Function(book As XElement) CType(book, String) = "Domain Driven Design") _
 .First()

' reverse the order since we want the topmost category first
Dim ancestors As IEnumerable(Of XElement) = dddBook.Ancestors("category").Reverse()

' join each category with a /
Dim categoryPath As String = String.Join("/", ancestors.Reverse().Select(Function(e As XElement) CType(e.Attribute("name"), String)).ToArray())

Console.WriteLine(CType(dddBook, String) + " is in the : " + categoryPath + " category.")