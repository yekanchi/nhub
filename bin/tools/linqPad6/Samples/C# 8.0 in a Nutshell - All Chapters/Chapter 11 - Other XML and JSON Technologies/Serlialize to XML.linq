<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	SerializeContacts();
	DeserializeContacts().Dump();
}

private void SerializeContacts()
{
	XmlWriterSettings settings = new XmlWriterSettings();
	settings.Indent = true; // To make visual inspection easier

	using XmlWriter writer = XmlWriter.Create ("contacts.xml", settings);

	Contacts cts = new Contacts()
	{
		Customers = new List<Customer>()
		{
			new Customer() { ID = 1, FirstName = "Sara", LastName = "Wells"},
			new Customer() { ID = 2, FirstName = "Dylan", LastName = "Lockwood"}
		},
		Suppliers = new List<Supplier>()
		{
			new Supplier() { Name = "Ian Weemes" }
		}
	};

	writer.WriteStartElement ("contacts");
	cts.WriteXml (writer);
	writer.WriteEndElement();
}

private Contacts DeserializeContacts()
{
	XmlReaderSettings settings = new XmlReaderSettings();
	settings.IgnoreWhitespace = true;
	settings.IgnoreComments = true;
	settings.IgnoreProcessingInstructions = true;

	using XmlReader reader = XmlReader.Create ("contacts.xml", settings);
	reader.MoveToContent();

	var cts = new Contacts();
	cts.ReadXml (reader);

	return cts;
}

public class Contacts
{
	public IList<Customer> Customers = new List<Customer>();
	public IList<Supplier> Suppliers = new List<Supplier>();

	public void ReadXml (XmlReader r)
	{
		bool isEmpty = r.IsEmptyElement;           // This ensures we don't get
		r.ReadStartElement();                      // snookered by an empty
		if (isEmpty) return;                       // <contacts/> element!
		while (r.NodeType == XmlNodeType.Element)
		{
			if (r.Name == Customer.XmlName) Customers.Add (new Customer (r));
			else if (r.Name == Supplier.XmlName) Suppliers.Add (new Supplier (r));
			else
				throw new XmlException ("Unexpected node: " + r.Name);
		}
		r.ReadEndElement();
	}

	public void WriteXml (XmlWriter w)
	{
		foreach (Customer c in Customers)
		{
			w.WriteStartElement (Customer.XmlName);
			c.WriteXml (w);
			w.WriteEndElement();
		}
		foreach (Supplier s in Suppliers)
		{
			w.WriteStartElement (Supplier.XmlName);
			s.WriteXml (w);
			w.WriteEndElement();
		}
	}

}

public class Customer
{
	public const string XmlName = "customer";
	public int? ID;
	public string FirstName, LastName;

	public Customer () { }
	public Customer (XmlReader r) { ReadXml (r); }

	public void ReadXml (XmlReader r)
	{
		if (r.MoveToAttribute ("id")) ID = r.ReadContentAsInt();
		r.ReadStartElement();
		FirstName = r.ReadElementContentAsString ("firstname", "");
		LastName = r.ReadElementContentAsString ("lastname", "");
		r.ReadEndElement();
	}

	public void WriteXml (XmlWriter w)
	{
		if (ID.HasValue) w.WriteAttributeString ("id", "", ID.ToString());
		w.WriteElementString ("firstname", FirstName);
		w.WriteElementString ("lastname", LastName);
	}
}

public class Supplier
{
	public const string XmlName = "supplier";
	public string Name;

	public Supplier () { }
	public Supplier (XmlReader r) { ReadXml (r); }

	public void ReadXml (XmlReader r)
	{
		r.ReadStartElement();
		Name = r.ReadElementContentAsString ("name", "");
		r.ReadEndElement();
	}

	public void WriteXml (XmlWriter w)
	{
		w.WriteElementString ("name", Name);
	}
}