<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
XElement xml = 
	new XElement("books",
	from line in File.ReadAllLines(Path + "books.txt.sdf")
	where !line.StartsWith("#")
	let items = line.Split(',')
	select new XElement("book",
			new XElement("title", items[1]),
				new XElement("authors",
				from authorFullName in items[2].Split(';')
				let authorNameParts = authorFullName.Split(' ')
				select new XElement("author",
				new XElement("firstName", authorNameParts[0]),
					new XElement("lastName", authorNameParts[1])
				)
			),
			new XElement("publisher", items[3]),
			new XElement("publicationDate", items[4]),
			new XElement("price", items[5]),
			new XElement("isbn", items[0])
			)
	);
Console.WriteLine(xml);