<Query Kind="Program">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>System.Data.Linq.Mapping</Namespace>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

void Main()
{
	
	// build our objects using query expressions and object initializers
	var books =
	from bookElement in this.BookXml().Elements("book")
			select new  LinqInAction.LinqBooks.Common.Book {
			Title=(string)bookElement.Element("title"),
			Publisher=new Publisher {
				Name=(string)bookElement.Element("publisher")
			},
			PublicationDate=(DateTime)bookElement.Element("publicationDate"),
			Price=(decimal) bookElement.Element("price"),
			Isbn = (string) bookElement.Element("isbn"),
			Notes=(string) bookElement.Element("notes"),
			Summary=(string) bookElement.Element("summary"),
			Authors=
			from authorElement in bookElement.Descendants("author")
			select new Author {
				FirstName=(string) authorElement.Element("firstName"),
				LastName=(string) authorElement.Element("lastName")
			},
			Reviews=
			from reviewElement in bookElement.Descendants("review")
			select new Review {
				User=new User { 
					Name =(string) reviewElement.Element("user")
				},
				Rating=(int) reviewElement.Element("rating"),
				Comments=(string) reviewElement.Element("comments")
			}
		};
		
	books.Dump();
	
	
}

// Define other methods and classes here
public XElement BookXml()
{
	XElement xml = XElement.Parse(
		@"<?xml version='1.0' encoding='utf-8' ?>
		<books>
			<book>
				<title>LINQ in Action</title>
			<authors>
			<author>
				<firstName>Fabrice</firstName>
				<lastName>Marguerie</lastName>
				<website>http://linqinaction.net/</website>
			</author>
			<author>
				<firstName>Steve</firstName>
				<lastName>Eichert</lastName>
				<webSite>http://iqueryable.com</webSite>
			</author>
			<author>
				<firstName>Jim</firstName>
				<lastName>Wooley</lastName>
				<webSite> http://devauthority.com/blogs/jwooley/</webSite>
			</author>
			</authors>
			<subject>
			<name>LINQ</name>
			<description>LINQ shall rule the world</description>
			</subject>
			<publisher>Manning</publisher>
			<publicationDate>November 15, 2007</publicationDate>
			<price>44.99</price>
			<isbn>1933988169</isbn>
			<notes>Great book!</notes>
			<summary>LINQ in Action is great!</summary>
				<reviews>
					<review>
						<user>Steve Eichert</user>
						<rating>5</rating>
						<comments>What can I say, I’m biased!</comments>
					</review>
				</reviews>
			</book>
		</books>");
	return xml;
}