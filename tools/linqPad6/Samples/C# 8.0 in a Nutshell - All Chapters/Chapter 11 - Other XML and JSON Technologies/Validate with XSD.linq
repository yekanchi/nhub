<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Xml.Schema</Namespace>
</Query>

#load ".\Create sample files.linq"

XmlReaderSettings settings = new XmlReaderSettings();
settings.ValidationType = ValidationType.Schema;
settings.Schemas.Add (null, "customers.xsd");

using (XmlReader r = XmlReader.Create ("customers.xml", settings))
	try { while (r.Read()) ; }
	catch (XmlSchemaValidationException ex)
	{
		$"Invalid XML according to schema: {ex.Message}".Dump();
	}
"Finished processing XML".Dump();