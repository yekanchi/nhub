<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement existingBooks = XElement.Load(Path + "existingBooks.xml.sdf");
XElement books = new XElement("books");
books.Add(existingBooks.Elements("book"));

books.Dump("Before Adding");

XElement newBook = new XElement("book", "LINQ in Action");
XElement firstBook = books.Element("book");
firstBook.AddAfterSelf(newBook);

books.Dump("After Adding");