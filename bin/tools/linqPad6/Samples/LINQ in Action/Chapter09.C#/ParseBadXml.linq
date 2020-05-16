<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

try
{
	XElement xml = XElement.Parse("<bad xml>");
}
catch (System.Xml.XmlException)
{
	Console.WriteLine("Unable to parse. Invalid XML");
}