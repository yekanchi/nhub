<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement root = XElement.Load(Path + "categorizedBooks.xml.sdf");
XElement dotNetCategory = root.XPathSelectElements("//category[@name='.NET']").First();
Console.WriteLine(dotNetCategory);