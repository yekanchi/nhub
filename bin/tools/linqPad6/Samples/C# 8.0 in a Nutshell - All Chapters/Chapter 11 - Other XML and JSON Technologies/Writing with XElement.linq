<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

XmlWriterSettings settings = new XmlWriterSettings() { Indent = true }; // Otherwise the XML is written as one very long line.
                                                                        // Saves space but makes it more difficult for humans.

using XmlWriter w = XmlWriter.Create ("logfile.xml", settings);

w.WriteStartElement ("log");
for (int i = 0; i < 1000000; i++)
{
	XElement e = new XElement ("logentry",
                   new XAttribute ("id", i),
                   new XElement ("date", DateTime.Today.AddDays (-1)),
                   new XElement ("source", "test"));
	e.WriteTo (w);
}
w.WriteEndElement ();

w.Dispose();
using var reader = File.OpenText("logfile.xml");
for (int i = 0; i < 10; i++) Console.WriteLine (reader.ReadLine());