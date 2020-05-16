<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
XElement.Load(Path & "books.xml.sdf").Dump
XElement.Load("http://msdn.microsoft.com/rss.xml", LoadOptions.PreserveWhitespace).Dump