<Query Kind="VBStatements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

Dim book As XElement = <book publicationDate="4/14/1978" title="Beauty"/>
Dim allAttributes As IEnumerable(Of XAttribute) = book.Attributes()
For Each attribute As XAttribute In allAttributes
  Console.WriteLine(attribute)
Next

Dim allPubDateAttributes As IEnumerable(Of XAttribute) = book.Attributes("publicationDate")
For Each pubDateAttribute As XAttribute In allPubDateAttributes
  Console.WriteLine(CType(pubDateAttribute, DateTime))
Next