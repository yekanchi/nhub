<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim doc As XmlDocument = New XmlDocument()
doc.Load("http://iqueryable.com/rss.aspx")

Dim ns As XmlNamespaceManager = New XmlNamespaceManager(doc.NameTable)
ns.AddNamespace("dc", "http://purl.org/dc/elements/1.1/")
ns.AddNamespace("slash", "http://purl.org/rss/1.0/modules/slash/")
ns.AddNamespace("wfw", "http://wellformedweb.org/CommentAPI/")

Dim commentNodes As XmlNodeList = doc.SelectNodes("//slash:comments", ns)

For Each node As XmlNode In commentNodes
  Console.WriteLine(node.InnerText)
Next

Dim titleNodes As XmlNodeList = doc.SelectNodes("/rss/channel/item/title")
For Each node As XmlNode In titleNodes
  Console.WriteLine(node.InnerText)
Next