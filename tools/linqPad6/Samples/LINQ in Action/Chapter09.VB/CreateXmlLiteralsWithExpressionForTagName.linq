<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim elementName As String = "book_tag"
Dim title As String = "NHibernate in Action"
Dim author As String = "Pierre Kuate"
Dim publisher As String = "Manning"

Dim book As XElement = <<%= elementName %>>
						 <title><%= title %></title>
						 <author><%= author %></author>
						 <publisher><%= publisher %></publisher>
					   </>

Console.WriteLine(book)