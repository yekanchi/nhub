<Query Kind="Program">
  <Namespace>System.Xml.Serialization</Namespace>
  <UseNoncollectibleLoadContext>true</UseNoncollectibleLoadContext>
</Query>

void Main()
{
	Person p = new Person { Name = "Stacey" };
	p.HomeAddress.Street = "Odo St";
	p.HomeAddress.PostCode = "6020";

	SerializePerson (p, "person.xml");
	File.ReadAllText ("person.xml").Dump ("XML");
}

public void SerializePerson (Person p, string path)
{
	XmlSerializer xs = new XmlSerializer (typeof (Person));
	using (Stream s = File.Create (path))
		xs.Serialize (s, p);
}

[XmlInclude (typeof (AUAddress))]
[XmlInclude (typeof (USAddress))]
public class Address { public string Street, PostCode; }

public class USAddress : Address { }
public class AUAddress : Address { }

public class Person
{
	public string Name;
	public Address HomeAddress = new USAddress();
}