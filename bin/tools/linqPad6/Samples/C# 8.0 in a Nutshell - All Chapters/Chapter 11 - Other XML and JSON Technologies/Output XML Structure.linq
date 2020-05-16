<Query Kind="Statements" />

#load ".\Create sample files.linq"

XmlReaderSettings settings = new XmlReaderSettings();
settings.IgnoreWhitespace = true;

using XmlReader reader = XmlReader.Create ("customer.xml", settings);
while (reader.Read())
{
	Console.Write (new string (' ', reader.Depth * 2));  // Write indentation
	Console.Write (reader.NodeType.ToString());

	if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
		Console.Write (" Name=" + reader.Name);
	else if (reader.NodeType == XmlNodeType.Text)
		Console.Write (" Value=" + reader.Value);
	
	Console.WriteLine ();
}