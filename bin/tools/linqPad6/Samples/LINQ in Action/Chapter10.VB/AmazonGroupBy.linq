<Query Kind="VBStatements">
  <Reference>G:\Projets\LINQ in Action\Content\Code\LINQPad\Chapter09and10.Common.dll</Reference>
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
parameters("Operation") = "TagLookup"
parameters("TagName") = "dotnet"
parameters("Count") = "20"
parameters("ResponseGroup") = "Tags,Small"
Dim signHelper = New AmazonProductAdvtApi.SignedRequestHelper(accessKey, secretKey, "ecs.amazonaws.com")
Dim url = signHelper.Sign(parameters)
Dim ns = Amazon.AmazonNS

Dim tags As XElement = XElement.Load(url)
Dim groups = From book In tags.Descendants(ns + "Item") _
	Let bookAttributes = book.Element(ns + "ItemAttributes") _
	Let title = CType(bookAttributes.Element(ns + "Title"), String) _
	Let publisher = CType(bookAttributes.Element(ns + "Manufacturer"), String) _
	Order By publisher, title _
	Group title By Key = publisher Into Group _
	Select Key, Group

For Each group In groups
  Console.WriteLine(group.Group.Count().ToString & " book(s) published by " & group.Key)
  For Each title As String In group.Group
	Console.WriteLine(" - " + title)
  Next
Next