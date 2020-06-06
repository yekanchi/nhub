<Query Kind="VBProgram">
  <Namespace>System.Xml.Xsl</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
</Query>

Sub Main
	Dim Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LINQPad\Samples\LINQ in Action\"
	Dim xsl As XDocument = <?xml version='1.0' encoding='UTF-8'?>
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
							   </xsl:stylesheet>
	
	Dim books As XElement = XElement.Load(PAth & "books.xml.sdf")
	Dim output As XDocument = New XDocument()
	
	Using writer As XmlWriter = output.CreateWriter()
	Dim xslTransformer As XslCompiledTransform = New XslCompiledTransform()
	  xslTransformer.Load(XmlReader.Create(New StringReader(xsl.ToString())))
	  xslTransformer.Transform(books.CreateReader(), writer)
	End Using
	
	Console.WriteLine(output)
	
	Console.WriteLine("With Extension Method")
	Console.WriteLine(XElement.Load(PAth & "books.xml.sdf").XslTransform(xsl.ToString()))
End Sub

End Class 'LINQ Pad hack to enable extension methods

' Define other methods and classes here
Module XmlExtensions
  <Extension()> _
  Function XslTransform(ByVal node As XNode, ByVal xsl As String) As XDocument
	Dim output As XDocument = New XDocument()
	Using writer As XmlWriter = output.CreateWriter()
	  Dim xslTransformer As XslCompiledTransform = New XslCompiledTransform()
	  xslTransformer.Load(XmlReader.Create(New StringReader(xsl)))
	  xslTransformer.Transform(node.CreateReader(), writer)
	End Using
	Return output
  End Function
End Module

Class Foo 'LINQ Pad hack to enable extension methods