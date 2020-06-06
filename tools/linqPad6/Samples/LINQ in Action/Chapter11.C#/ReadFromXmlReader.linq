<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
  <Namespace>System.Xml.Xsl</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement books = XElement.Load(Path + "books.xml.sdf");
XElement xml = new XElement("html",
		new XElement("title", "Book Catalog"),
		new XElement("body",
				new XElement("ul",
						from b in books.Elements("book")
						select new XElement("li", (string) b.Element("title") + " by " + 
							String.Join(", ", (from a in b.Elements("author") select (string) a).ToArray())
						)
				)
		)
);
xml.Save(Path + "books.html");

var xsl = new XslCompiledTransform();
xsl.Load(Path + "booksToXHTML.xsl.sdf");
xsl.Transform(Path + "books.xml.sdf", Path + "books-xslt.html");
System.Diagnostics.Process.Start(Path + "books-xslt.html");