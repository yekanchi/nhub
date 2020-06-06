<Query Kind="VBStatements">
  <Reference>&lt;ApplicationData&gt;\LINQPad\Samples\LINQ in Action\LinqInAction.LinqBooks.Common.dll</Reference>
  <Namespace>LinqInAction.LinqBooks.Common</Namespace>
  <Namespace>Microsoft.VisualBasic.Information</Namespace>
</Query>

Dim rss = _
		 <?xml version="1.0" encoding="utf-8"?>
		 <rss version="2.0">
		   <channel>
			 <title>Book Reviews</title>
			 <description>LinqBook Book Reviews</description>
			 <%= From book In SampleData.Books _
			   Where Not IsNothing(book.Reviews) AndAlso book.Reviews.Count > 0 _
			   Select _
			   From review In book.Reviews _
			   Select _
			   <item>
				 <title>Review of <%= book.Title %> by <%= review.User.Name %></title>
				 <pubDate>Sun, 23, 2006, 02:19:00 GMT</pubDate>
				 <description><%= review.Comments %></description>
			   </item> %>
		   </channel>
		 </rss>
Console.WriteLine(rss)