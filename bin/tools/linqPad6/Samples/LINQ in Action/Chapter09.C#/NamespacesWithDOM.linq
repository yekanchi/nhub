<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XmlDocument doc = new XmlDocument();
doc.Load("http://iqueryable.com/rss.aspx");

XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
ns.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
ns.AddNamespace("slash", "http://purl.org/rss/1.0/modules/slash/");
ns.AddNamespace("wfw", "http://wellformedweb.org/CommentAPI/");

XmlNodeList commentNodes = doc.SelectNodes("//slash:comments", ns);
foreach (XmlNode node in commentNodes)
	Console.WriteLine(node.InnerText);


XmlNodeList titleNodes = doc.SelectNodes("/rss/channel/item/title");
foreach (XmlNode node in titleNodes)
	Console.WriteLine(node.InnerText);