<Query Kind="VBExpression">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

New XDocument( _
	 New XDeclaration("1.0", "utf-8", "yes"), _
	 New XProcessingInstruction("XML-stylesheet", "friendly-rss.xsl"), _
	 New XElement("rss", _
		New XElement("channel", "my channel") _
		) _
	 )