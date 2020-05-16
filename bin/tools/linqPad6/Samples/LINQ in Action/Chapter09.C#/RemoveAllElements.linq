<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement books = XElement.Load(Path + "existingBooks.xml.sdf");
books.SetElementValue("book", null);
Console.WriteLine(books);