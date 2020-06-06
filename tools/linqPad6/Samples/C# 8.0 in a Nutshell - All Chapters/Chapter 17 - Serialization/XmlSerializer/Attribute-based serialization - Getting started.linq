<Query Kind="Program">
  <Namespace>System.Xml.Serialization</Namespace>
  <UseNoncollectibleLoadContext>true</UseNoncollectibleLoadContext>
</Query>

void Main()
{
	Person p = new Person();
	p.Name = "Stacey"; p.Age = 30;

	var xs = new XmlSerializer (typeof (Person));

	using (Stream s = File.Create ("person.xml"))
		xs.Serialize (s, p);

	Person p2;
	using (Stream s = File.OpenRead ("person.xml"))
		p2 = (Person)xs.Deserialize (s);

	Console.WriteLine (p2.Name + " " + p2.Age);   // Stacey 30
	
	File.ReadAllText ("person.xml").Dump ("XML");
}

public class Person
{
	public string Name;
	public int Age;
}