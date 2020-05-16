<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Dim rss = XElement.Load(Path & "rss.xml.sdf")

Dim items = rss.<channel>(0).<item>
For Each item As XElement In items
  Console.WriteLine(CType(item.<title>.Value, String))
Next