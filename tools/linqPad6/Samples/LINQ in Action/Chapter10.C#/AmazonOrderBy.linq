<Query Kind="Statements">
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
	{ "Operation", "TagLookup" },
	{ "TagName", "dotnet" },
	{ "Count", "20" },
	{ "ResponseGroup", "Tags,Small" }
};
var signHelper = new AmazonProductAdvtApi.SignedRequestHelper(accessKey, secretKey, "ecs.amazonaws.com");
var url = signHelper.Sign(parameters);
var ns = Amazon.AmazonNS;

XElement tags = XElement.Load(url);
var titles = from book in tags.Descendants(ns + "Item")
		   let bookAttributes = book.Element(ns + "ItemAttributes")
		   let title = (string)bookAttributes.Element(ns + "Title")
		   orderby title
		   select title;

titles.Dump();