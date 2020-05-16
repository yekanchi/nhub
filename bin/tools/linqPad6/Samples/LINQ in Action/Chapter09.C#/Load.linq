<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";

XElement x = XElement.Load(Path + "books.xml.sdf");
Console.WriteLine(x);

XElement x2 = XElement.Load("http://msdn.microsoft.com/rss.xml", System.Xml.Linq.LoadOptions.PreserveWhitespace);
Console.WriteLine(x2);