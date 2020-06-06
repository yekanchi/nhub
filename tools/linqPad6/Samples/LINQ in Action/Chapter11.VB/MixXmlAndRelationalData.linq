<Query Kind="VBStatements">
  <Connection>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\lia.mdf</AttachFileName>
    <ID>ff5b77a9-8b82-4bab-a7bb-9e8802dcfd12</ID>
  </Connection>
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\Chapter09and10.Common.dll</Reference>
  <Namespace>System.Data.Linq.Mapping</Namespace>
  <Namespace>Chapter09and10.Common</Namespace>
</Query>

' In order to use the Amazon.com web services and test these examples,
' you need to register with the Amazon Web Services program.
' After registering with Amazon you will be assigned authentication keys.
' Edit this file and enter your keys.
' See http://docs.amazonwebservices.com/AWSECommerceService/latest/DG/index.html?RequestAuthenticationArticle.html

Const accessKey = INSERT YOUR AWS ACCESS KEY HERE
Const secretKey = INSERT YOUR AWS SECRET KEY HERE

Dim parameters = New Dictionary(Of String, String)()
parameters("Service") = Amazon.ServiceName
parameters("Version") = Amazon.ServiceVersion
parameters("Operation") = "ItemLookup"
parameters("ItemId") = "0735621632"
parameters("ResponseGroup") = "Reviews"
Dim signHelper = New AmazonProductAdvtApi.SignedRequestHelper(accessKey, secretKey, "ecs.amazonaws.com")
Dim url = signHelper.Sign(parameters)
Dim ns = Amazon.AmazonNS

Dim amazonReviews As XElement = XElement.Load(url)
Dim results = From bookElement In amazonReviews.Descendants(ns + "Items").Elements(ns + "Item") _
				Join book In Books On CType(bookElement.Element(ns + "ASIN"), String) Equals book.Isbn.Trim()  _
				Select New With { _
					.Title = book.Title, _
					.Reviews = _
					From reviewElement In bookElement.Descendants(ns + "Review") _
					Order By CType(reviewElement.Element(ns + "Rating"), Integer) Descending _
					Select New Review With { _
						.Rating = CType(reviewElement.Element(ns + "Rating"), Integer), _
						.Comments = CType(reviewElement.Element(ns + "Content"), String) _
						} _
					}

Dim seperator As String = "--------------------------"
For Each item In results
  Console.WriteLine("Book: " + item.Title)
  For Each review In item.Reviews
	Console.WriteLine(seperator + "\r\nRating: " + review.Rating + "\r\n" + seperator + "\r\n" + review.Comments)
  Next
Next