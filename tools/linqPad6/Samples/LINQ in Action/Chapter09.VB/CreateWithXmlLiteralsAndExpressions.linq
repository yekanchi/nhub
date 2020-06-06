<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim title As String = "NHibernate in Action"
Dim author As String = "Pierre Kuate"
Dim publisher As String = "Manning"

Dim book As XElement = <book>
						 <title><%= title %></title>
						 <author><%= author %></author>
						 <publisher><%= publisher %></publisher>
					   </book>

Console.WriteLine(book)