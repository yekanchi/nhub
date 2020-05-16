<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement root = XElement.Load(Path + "categorizedBooks.xml.sdf");
XElement dddBook = root.Descendants("book")
							  .Where(book => (string)book == "Domain Driven Design")
							  .First();

IEnumerable<XElement> beforeSelf = dddBook.ElementsBeforeSelf();
beforeSelf.Dump();