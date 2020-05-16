<Query Kind="Statements" />

// This creates the sample files used in Chapter 11 for XML.
// This query is #load-ed by other queries, so you don't need to run it directly.
// The files are written to the current (temp) folder, so they're cleaned up when you exit LINQPad

var contacts = @"<?xml version=""1.0"" encoding=""utf-8""?>
<contacts>
	<customer id=""1"">
		<firstname>Sara</firstname>
		<lastname>Wells</lastname>
	</customer>
	<customer id=""2"">
		<firstname>Dylan</firstname>
		<lastname>Lockwood</lastname>
	</customer>
	<supplier>
		<name>Ian Co.</name>
	</supplier>
</contacts>";

if (!File.Exists ("contacts.xml")) File.WriteAllText ("contacts.xml", contacts);

var customer = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<customer id=""123"" status=""archived"">
	<firstname>Jim</firstname>
	<lastname>Bo</lastname>
</customer>";

if (!File.Exists ("customer.xml")) File.WriteAllText ("customer.xml", customer);

var customerCredit = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<customer id=""123"" status=""archived"">
	<firstname>Jim</firstname>
	<lastname>Bo</lastname>
	<creditlimit>500.00</creditlimit>    <!-- OK, we sneaked this in! -->
</customer>";

if (!File.Exists ("customerCredit.xml")) File.WriteAllText ("customerCredit.xml", customerCredit);

var customerWithCDATA = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<!DOCTYPE customer [ <!ENTITY tc ""Top Customer""> ]>
<customer id=""123"" status=""archived"">
	<firstname>Jim</firstname>
	<lastname>Bo</lastname>
	<quote><![CDATA[C#'s operators include: < > &]]></quote>
	<notes>Jim Bo is a &tc;</notes>
	<!--  That wasn't so bad! -->
</customer>";

if (!File.Exists ("customerWithCDATA.xml")) File.WriteAllText ("customerWithCDATA.xml", customerWithCDATA);

var customers = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<customers>
	<customer id=""123"" status=""archived"">
		<firstname>Jim</firstname>
		<lastname>Bo</lastname>
	</customer>
	<customer id=""125"" status=""archived"">
		<firstname>Todd</firstname>
		<lastname>Bar</lastname>
	</customer>
</customers>";

if (!File.Exists ("customers.xml")) File.WriteAllText ("customers.xml", customers);

var logfile = @"<?xml version=""1.0"" encoding=""utf-8""?>
<log>
<logentry id=""0""><date>2019-10-11T00:00:00-07:00</date><source>test</source></logentry>
<logentry id=""1""><date>2019-10-11T00:00:01-07:00</date><source>test</source></logentry>
<logentry id=""2""><date>2019-10-11T00:00:02-07:00</date><source>test</source></logentry>
<logentry id=""3""><date>2019-10-11T00:00:03-07:00</date><source>test</source></logentry>
<logentry id=""4""><date>2019-10-11T00:00:05-07:00</date><source>test</source></logentry>
<logentry id=""5""><date>2019-10-11T00:00:08-07:00</date><source>test</source></logentry>
<logentry id=""6""><date>2019-10-11T00:00:09-07:00</date><source>test</source></logentry>
<logentry id=""7""><date>2019-10-11T00:00:10-07:00</date><source>test</source></logentry>
<logentry id=""8""><date>2019-10-11T00:00:13-07:00</date><source>test</source></logentry>
<logentry id=""9""><date>2019-10-11T00:00:14-07:00</date><source>test</source></logentry>
</log>";

if (!File.Exists ("logfile.xml")) File.WriteAllText ("logfile.xml", logfile);

var customersSchema = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xs:schema attributeFormDefault=""unqualified""
           elementFormDefault=""qualified""
           xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
	<xs:element name=""customers"">
		<xs:complexType>
			<xs:sequence>
				<xs:element maxOccurs=""unbounded"" name=""customer"">
					<xs:complexType>
						<xs:sequence>
							<xs:element name=""firstname"" type=""xs:string"" />
							<xs:element name=""lastname"" type=""xs:string"" />
						</xs:sequence>
						<xs:attribute name=""id"" type=""xs:int"" use=""required"" />
						<xs:attribute name=""status"" type=""xs:string"" use=""required"" />
					</xs:complexType>
				</xs:element>
			</xs:sequence>
		</xs:complexType>
	</xs:element>
</xs:schema>
";

if (!File.Exists ("customers.xsd")) File.WriteAllText ("customers.xsd", customersSchema);


var customerXslt = @"<?xml version=""1.0"" encoding=""UTF-8""?>
	<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform""
version=""1.0"">
	<xsl:template match=""/"">
		<html>
			<p><xsl:value-of select=""//firstname""/></p>
			<p><xsl:value-of select=""//lastname""/></p>
		</html>
	</xsl:template>
</xsl:stylesheet>
";

if (!File.Exists ("customer.xslt")) File.WriteAllText ("customer.xslt", customerXslt);