<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement books = XElement.Load(Path + "books.xml.sdf");

var manningBooks =
			from b in books.Elements("book")
			where (string) b.Element("publisher") == "Manning"
			orderby (string) b.Element("title")
			select b.Element("title");
manningBooks.Dump();

List<string> titles = new List<string>();
XmlDocument doc = new XmlDocument();
doc.Load(Path + "books.xml.sdf");
XmlNodeList nodes = doc.SelectNodes("/books/book");
foreach (XmlNode node in nodes) {
	if (node.SelectSingleNode("publisher").InnerText == "Manning") {
		titles.Add(node.SelectSingleNode("title").InnerText);
	}
	titles.Sort();
}
titles.Dump();