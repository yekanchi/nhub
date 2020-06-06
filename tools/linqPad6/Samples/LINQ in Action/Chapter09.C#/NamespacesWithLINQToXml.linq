<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement rss = XElement.Load("http://iqueryable.com/rss.aspx");
XNamespace dc = "http://purl.org/dc/elements/1.1/";
XNamespace slash = "http://purl.org/rss/1.0/modules/slash/";
XNamespace wfw = "http://wellformedweb.org/CommentAPI/";

IEnumerable<XElement> comments = rss.Descendants(slash + "comments");
foreach (XElement comment in comments)
	Console.WriteLine((int)comment);


IEnumerable<XElement> titles = rss.Descendants("title");
foreach (XElement title in titles)
	Console.WriteLine((string)title);