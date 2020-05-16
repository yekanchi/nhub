<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XElement book = XElement.Parse("<book publicationDate=\"4/14/1978\" title=\"Beauty\"/>");
IEnumerable<XAttribute> allAttributes = book.Attributes();
foreach (XAttribute attribute in allAttributes)
	Console.WriteLine(attribute);


IEnumerable<XAttribute> allPubDateAttributes = book.Attributes("publicationDate");
foreach (XAttribute pubDateAttribute in allPubDateAttributes)
	Console.WriteLine((DateTime)pubDateAttribute);