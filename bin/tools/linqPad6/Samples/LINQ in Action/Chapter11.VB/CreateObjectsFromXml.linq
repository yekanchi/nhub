<Query Kind="VBProgram">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
</Query>

Sub Main()
	' build our objects using query expressions and object initializers
	Dim books = _
		From bookElement In BookXml().Elements("book") _
		Select New Book With { _
			.Title = CType(bookElement.Element("title"), String), _
			.Publisher = New Publisher With {.Name = CType(bookElement.Element("publisher"), String)}, _
			.PublicationDate = CType(bookElement.Element("publicationDate"), DateTime), _
			.Price = CType(bookElement.Element("price"), Decimal), _
			.Isbn = CType(bookElement.Element("isbn"), String), _
			.Notes = CType(bookElement.Element("notes"), String), _
			.Summary = CType(bookElement.Element("summary"), String), _
			.Authors = GetAuthors(bookElement), _
			.Reviews = GetReviews(bookElement) _
			}

		' print out the results
	books.Dump()
End Sub

Function GetAuthors(ByVal bookElement As XElement) As IEnumerable(Of Author)
	Return (From authorElement In bookElement.Descendants("author") _
	 Select New Author With {.FirstName = CType(authorElement.Element("firstName"), String), .LastName = CType(authorElement.Element("lastName"), String)}).ToList()
End Function

Function GetReviews(ByVal bookElement As XElement) As IEnumerable(Of Review)
	Return (From reviewElement In bookElement.Descendants("review") _
		Select New Review With { _
			.User = New User With { _
				.Name = CType(reviewElement.Element("user"), String) _
				}, _
			.Rating = CType(reviewElement.Element("rating"), Integer), _
			.Comments = CType(reviewElement.Element("comments"), String) _
			}).ToList()
End Function

Function BookXml() As XElement
	Dim xml as XElement = _
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
			</books>
	Return xml
End Function