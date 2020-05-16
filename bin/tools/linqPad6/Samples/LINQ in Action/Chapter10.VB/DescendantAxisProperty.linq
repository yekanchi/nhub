<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Dim rss = XElement.Load(Path & "rss.xml.sdf")

Dim items = rss...<item>

For Each item As XElement In items
  Console.WriteLine(item.<title>.Value)
Next