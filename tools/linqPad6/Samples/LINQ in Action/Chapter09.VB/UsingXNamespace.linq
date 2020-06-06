<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

  Dim ns As XNamespace = "http://linqinaction.net"
	Dim book As XElement = New XElement(ns + "book", _
	 New XElement(ns + "title", "LINQ in Action"), _
	 New XElement(ns + "author", "Fabrice Marguerie"), _
	 New XElement(ns + "publisher", "Manning") _
	)
	
Console.WriteLine(book)