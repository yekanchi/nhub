<Query Kind="Expression">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

new XDocument(
			  new XProcessingInstruction("XML-stylesheet", "href='http://iqueryable.com/friendly-rss.xsl' type='text/xsl' media='screen'"),
			  new XElement("rss", new XAttribute("version", "2.0"),
				new XElement("channel",
				  new XElement("item", "my item")
				)
			  )
			)