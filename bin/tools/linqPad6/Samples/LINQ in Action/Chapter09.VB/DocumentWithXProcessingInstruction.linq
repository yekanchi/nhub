<Query Kind="VBExpression">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

New XDocument( _
	 New XProcessingInstruction("XML-stylesheet", "href='http://iqueryable.com/friendly-rss.xsl' type='text/xsl' media='screen'"), _
	 New XElement("rss", New XAttribute("version", "2.0"), _
		New XElement("channel", _
			New XElement("item", "my item") _
			) _
		) _
	 )