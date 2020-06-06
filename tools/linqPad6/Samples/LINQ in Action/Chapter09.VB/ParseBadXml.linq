<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Try
  Dim xml As XElement = XElement.Parse("<bad xml>")
Catch e As System.Xml.XmlException
  Console.WriteLine("Exception caught: " & e.ToString)
End Try