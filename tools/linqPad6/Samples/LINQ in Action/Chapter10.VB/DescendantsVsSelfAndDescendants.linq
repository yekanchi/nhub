<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Dim root As XElement = XElement.Load(Path & "categorizedBooks.xml.sdf")
Dim categories As IEnumerable(Of XElement) = root.Descendants("category")

Console.WriteLine("Descendants")
For Each categoryElement As XElement In categories
  Console.WriteLine(" - " + CType(categoryElement.Attribute("name"), String))
Next

categories = root.DescendantsAndSelf("category")
Console.WriteLine("DescendantsAndSelf")
For Each categoryElement As XElement In categories
  Console.WriteLine(" - " + CType(categoryElement.Attribute("name"), String))
Next