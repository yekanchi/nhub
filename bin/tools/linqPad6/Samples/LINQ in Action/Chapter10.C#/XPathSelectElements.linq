<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement root = XElement.Load(Path + "categorizedBooks.xml.sdf");
var books = from book in root.XPathSelectElements("//book")
		    select book;

books.Dump();