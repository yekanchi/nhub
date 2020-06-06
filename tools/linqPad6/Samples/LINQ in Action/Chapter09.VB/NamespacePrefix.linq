<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim ns As XNamespace = "http://linqinaction.net"
Dim book As XElement = New XElement(ns + "book", _
 New XAttribute(XNamespace.Xmlns + "l", ns) _
)

Console.WriteLine(book)