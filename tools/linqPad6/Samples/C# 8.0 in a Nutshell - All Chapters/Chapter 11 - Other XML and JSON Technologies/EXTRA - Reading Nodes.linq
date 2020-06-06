<Query Kind="Statements" />

#load ".\Create sample files.linq"

XmlReaderSettings settings = new XmlReaderSettings();
settings.IgnoreWhitespace = true;
settings.DtdProcessing = DtdProcessing.Parse;    // Required to read DTDs

using XmlReader r = XmlReader.Create ("customerWithCDATA.xml", settings);
while (r.Read())
{
	Console.Write (r.NodeType.ToString().PadRight (17, '-'));
	Console.Write ("> ".PadRight (r.Depth * 3));

	switch (r.NodeType)
	{
		case XmlNodeType.Element:
		case XmlNodeType.EndElement:
			Console.WriteLine (r.Name); break;

		case XmlNodeType.Text:
		case XmlNodeType.CDATA:
		case XmlNodeType.Comment:
		case XmlNodeType.XmlDeclaration:
			Console.WriteLine (r.Value); break;

		case XmlNodeType.DocumentType:
			Console.WriteLine (r.Name + " - " + r.Value); break;

		default: break;
	}
}