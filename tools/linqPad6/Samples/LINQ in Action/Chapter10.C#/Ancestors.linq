<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement root = XElement.Load(Path + "categorizedBooks.xml.sdf");
XElement dddBook = root.Descendants("book")
					.Where(book => (string)book == "Domain Driven Design")
					.First();

// reverse the order since we want the topmost category first
IEnumerable<XElement> ancestors = dddBook.Ancestors("category").Reverse();

// join each category with a /
string categoryPath = String.Join("/", ancestors.Reverse().Select(e => (string)e.Attribute("name")).ToArray());

Console.WriteLine((string)dddBook + " is in the : " + categoryPath + " category.");