<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim book As XElement = <book publisher='Manning'>LINQ in Action</book>

Console.WriteLine(book.@publisher)