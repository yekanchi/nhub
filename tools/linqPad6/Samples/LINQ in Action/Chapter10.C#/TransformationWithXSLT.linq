<Query Kind="Program">
  <Namespace>System.Data.Linq.Mapping</Namespace>
  <Namespace>System.Xml.Xsl</Namespace>
</Query>

void Main()
{
	 string xsl = @"<?xml version='1.0' encoding='UTF-8' ?>
					<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
	                  <xsl:template match='books'>
	                       <html>
	                            <title>Book Catalog</title>
	                            <ul>
		                            <xsl:apply-templates select='book'/>
	                            </ul>
	                       </html>
	                  </xsl:template>
	                  <xsl:template match='book'>
	                       <li>
	                            <xsl:value-of select='title'/> by <xsl:apply-templates select='author'/>
	                       </li>
	                  </xsl:template>
	                  <xsl:template match='author'>
	                       <xsl:if test='position() > 1'>, </xsl:if>
	                       <xsl:value-of select='.'/>
	                  </xsl:template>
					</xsl:stylesheet>";
	
	string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LINQPad\Samples\LINQ in Action\";
	XElement books = XElement.Load(Path + "books.xml.sdf");
	XDocument output = new XDocument();
	using (XmlWriter writer = output.CreateWriter())
	{
		XslCompiledTransform xslTransformer = new XslCompiledTransform();
		xslTransformer.Load(XmlReader.Create(new StringReader(xsl)));
		xslTransformer.Transform(books.CreateReader(), writer);
	}
	//   Console.WriteLine(output);
	
	Console.WriteLine(XElement.Load(Path + "books.xml.sdf").XslTransform(xsl));
}
} //Closing off this class to allow for extension method class

// Define other methods and classes here
  public static class XmlExtensions
  {
	public static XDocument XslTransform(this XNode node, string xsl)
	{
	  XDocument output = new XDocument();
	  using (XmlWriter writer = output.CreateWriter())
	  {
		XslCompiledTransform xslTransformer = new XslCompiledTransform();
		xslTransformer.Load(XmlReader.Create(new StringReader(xsl)));
		xslTransformer.Transform(node.CreateReader(), writer);
	  }
	  return output;
	}
  }
  
// Fake class definition to get LINQPad to allow us to define the extension class
public class Foo
{