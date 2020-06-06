<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XNamespace ns = "http://linqinaction.net";
XElement book = new XElement(ns + "book",
	new XAttribute(XNamespace.Xmlns + "l", ns)
);

Console.WriteLine(book);