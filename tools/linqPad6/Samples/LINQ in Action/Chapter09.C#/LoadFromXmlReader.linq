<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
using (XmlReader reader = XmlReader.Create(Path + "books.xml.sdf"))
{
	while (reader.Read())
	{
		if (reader.NodeType == XmlNodeType.Element)
			break;
	}
	XElement bookXml = (XElement)XNode.ReadFrom(reader);
	Console.WriteLine(bookXml);
}