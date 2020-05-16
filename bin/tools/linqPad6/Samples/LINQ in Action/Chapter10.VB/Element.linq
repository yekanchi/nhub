<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Dim root As XElement = XElement.Load(Path & "categorizedBooks.xml.sdf")
Dim dotNetCategory As XElement = root.Element("category")
Console.WriteLine(dotNetCategory)