<Query Kind="Program">
  <Namespace>System.Xml.Serialization</Namespace>
  <UseNoncollectibleLoadContext>true</UseNoncollectibleLoadContext>
</Query>

void Main()
{
	Person p = new Person { Name = "Stacey" };
	p.Addresses.Add (new Address { Street = "My Street", PostCode = "1234" });
	p.Addresses.Add (new Address { Street = "My Way", PostCode = "2345" });

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

	[XmlArray ("PreviousAddresses")]
	[XmlArrayItem ("Location")]
	public List<Address> Addresses = new List<Address>();
}

public class Address { public string Street, PostCode; }