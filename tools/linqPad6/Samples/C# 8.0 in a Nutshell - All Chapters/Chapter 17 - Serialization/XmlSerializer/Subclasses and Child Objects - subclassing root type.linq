<Query Kind="Program">
  <Namespace>System.Xml.Serialization</Namespace>
  <UseNoncollectibleLoadContext>true</UseNoncollectibleLoadContext>
</Query>

void Main()
{
	var p = new Student { Name = "Stacey" };
	SerializePerson (p, "person.xml");
	File.ReadAllText ("person.xml").Dump ("XML");
}

public void SerializePerson (Person p, string path)
{
	XmlSerializer xs = new XmlSerializer (typeof (Person));
	using (Stream s = File.Create (path))
		xs.Serialize (s, p);
}

[XmlInclude (typeof (Student))]
[XmlInclude (typeof (Teacher))]
public class Person { public string Name; }

public class Student : Person { }
public class Teacher : Person { }