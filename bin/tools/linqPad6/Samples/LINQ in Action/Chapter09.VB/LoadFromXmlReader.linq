<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
Using reader As XmlReader = XmlReader.Create(path & "books.xml.sdf")
  While reader.Read
	If reader.NodeType = XmlNodeType.Element Then
	  Exit While
	End If
  End While

  Dim booksXml As XElement = XElement.Load(reader)
  Console.WriteLine(booksXml)
End Using