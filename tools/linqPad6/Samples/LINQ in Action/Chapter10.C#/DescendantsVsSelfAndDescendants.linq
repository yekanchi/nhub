<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement root = XElement.Load(Path + "categorizedBooks.xml.sdf");
IEnumerable<XElement> categories = root.Descendants("category");

Console.WriteLine("Descendants");
foreach (XElement categoryElement in categories)
	Console.WriteLine(" - " + (string)categoryElement.Attribute("name"));


categories = root.DescendantsAndSelf("category");
Console.WriteLine("DescendantsAndSelf");
foreach (XElement categoryElement in categories)
	Console.WriteLine(" - " + (string)categoryElement.Attribute("name"));