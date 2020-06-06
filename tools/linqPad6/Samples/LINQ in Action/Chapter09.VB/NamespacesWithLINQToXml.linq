<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim rss As XElement = XElement.Load("http://iqueryable.com/rss.aspx")
Dim dc As XNamespace = "http://purl.org/dc/elements/1.1/"
Dim slash As XNamespace = "http://purl.org/rss/1.0/modules/slash/"
Dim wfw As XNamespace = "http://wellformedweb.org/CommentAPI/"

Dim comments As IEnumerable(Of XElement) = rss.Descendants(slash + "comments")
For Each comment As XElement In comments
  Console.WriteLine(CType(comment, Int32))
Next

Dim titles As IEnumerable(Of XElement) = rss.Descendants("title")
For Each title As XElement In titles
  Console.WriteLine(CType(title, String))
Next
