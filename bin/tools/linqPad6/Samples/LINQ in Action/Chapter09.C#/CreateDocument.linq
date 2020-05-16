<Query Kind="Statements">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

XDocument doc = new XDocument(
			  new XDeclaration("1.0", "utf-8", "yes"),
			  new XProcessingInstruction("XML-stylesheet", "friendly-rss.xsl"),
			  new XElement("rss",
				new XElement("channel", "my channel")
			  )
			);
			
Console.WriteLine(doc);