<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"

Using reader As XmlReader = XmlTextReader.Create(path & "books.xml.sdf")
  While reader.Read
	If reader.NodeType = XmlNodeType.Element And reader.Name = "book" Then
	  Exit While
	End If
  End While
  Dim bookXml As XElement = CType(XNode.ReadFrom(reader), XElement)
  Console.WriteLine(bookXml)
End Using