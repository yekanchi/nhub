<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

#load ".\Create sample files.linq"

XmlReaderSettings settings = new XmlReaderSettings();
settings.IgnoreWhitespace = true;

using XmlReader r = XmlReader.Create ("logfile.xml", settings);

r.ReadStartElement ("log");
while (r.Name == "logentry")
{
	XElement logEntry = (XElement)XNode.ReadFrom (r);
	int id = (int)logEntry.Attribute ("id");
	DateTime date = (DateTime)logEntry.Element ("date");
	string source = (string)logEntry.Element ("source");
	$"{id} {date} {source}".Dump();
}
r.ReadEndElement();