<Query Kind="Program">
  <Namespace>System.Xml.Serialization</Namespace>
  <Namespace>System.Xml.Schema</Namespace>
  <UseNoncollectibleLoadContext>true</UseNoncollectibleLoadContext>
</Query>

void Main()
{
	Person p = new Person 
	{
		Name = "Stacey",
		HomeAddress = new Address { Street = "My Street", PostCode = "90210" }
	};

	SerializePerson (p, "person.xml");
	File.ReadAllText ("person.xml").Dump ("XML");
}

public void SerializePerson (Person p, string path)
{
	XmlSerializer xs = new XmlSerializer (typeof (Person));
	using (Stream s = File.Create (path))
		xs.Serialize (s, p);
}

public class Person
{
	public string Name;
	public Address HomeAddress;
}

public class Address : IXmlSerializable
{
	public string Street, PostCode;

	public XmlSchema GetSchema() { return null; }

	public void ReadXml (XmlReader reader)
	{
		reader.ReadStartElement();
		Street = reader.ReadElementContentAsString ("Street", "");
		PostCode = reader.ReadElementContentAsString ("PostCode", "");
		reader.ReadEndElement();
	}

	public void WriteXml (XmlWriter writer)
	{
		writer.WriteElementString ("Street", Street);
		writer.WriteElementString ("PostCode", PostCode);
	}
}