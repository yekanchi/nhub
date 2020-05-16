<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XNamespace ns = "http://linqinaction.net";
XElement book = new XElement(ns + "book",
	new XElement(ns + "title", "LINQ in Action"),
	new XElement(ns + "author", "Fabrice Marguerie"),
	new XElement(ns + "author", "Steve Eichert"),
	new XElement(ns + "author", "Jim Wooley"),
	new XElement(ns + "publisher", "Manning")
);

Console.WriteLine(book);