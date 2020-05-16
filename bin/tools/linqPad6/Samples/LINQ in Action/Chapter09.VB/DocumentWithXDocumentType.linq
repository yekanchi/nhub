<Query Kind="VBExpression">
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

New XDocument( _
	 New XDocumentType("HTML", "-//W3C//DTD HTML 4.01//EN", _
	  "http://www.w3.org/TR/html4/strict.dtd", Nothing), _
	 New XElement("html", _
	 New XElement("body", "This is the body!") _
	 ) _
	 )