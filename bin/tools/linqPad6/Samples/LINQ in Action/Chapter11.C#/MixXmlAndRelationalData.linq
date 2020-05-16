<Query Kind="Statements">
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

// In order to use the Amazon.com web services and test these examples,
// you need to register with the Amazon Web Services program.
// After registering with Amazon you will be assigned authentication keys.
// Edit this file and enter your keys.
// See http://docs.amazonwebservices.com/AWSECommerceService/latest/DG/index.html?RequestAuthenticationArticle.html

String accessKey = INSERT YOUR AWS ACCESS KEY HERE;
String secretKey = INSERT YOUR AWS SECRET KEY HERE;
var parameters = new Dictionary<String, String>() {
		{ "Service", Amazon.ServiceName},
		{ "Version", Amazon.ServiceVersion },
		{ "Operation", "ItemLookup" },
		{ "ItemId", "0735621632" },
		{ "ResponseGroup", "Reviews" }
};
var signHelper = new AmazonProductAdvtApi.SignedRequestHelper(accessKey, secretKey, "ecs.amazonaws.com");
var url = signHelper.Sign(parameters);
var ns = Amazon.AmazonNS;

XElement amazonReviews = XElement.Load(url);

var results =
	from bookElement in amazonReviews.Element(ns + "Items").Elements(ns+ "Item")
	join book in Books on (string)bookElement.Element(ns+ "ASIN") equals book.Isbn.Trim()
	select new {
		    Title=book.Title, 
		    Reviews=
		      	from reviewElement in bookElement.Descendants(ns + "Review")
				orderby (int) reviewElement.Element(ns + "Rating") descending
		      	select new Review {
					  	Rating=(int) reviewElement.Element(ns+ "Rating"), 
		        		Comments=(string) reviewElement.Element(ns+ "Content")
				}
	};

string seperator = "--------------------------";
foreach (var item in results) {
	Console.WriteLine("Book: " + item.Title);
	foreach (var review in item.Reviews) {
  		Console.WriteLine(seperator + "\r\nRating: " + review.Rating + "\r\n" + seperator + "\r\n" + review.Comments);
	}
}