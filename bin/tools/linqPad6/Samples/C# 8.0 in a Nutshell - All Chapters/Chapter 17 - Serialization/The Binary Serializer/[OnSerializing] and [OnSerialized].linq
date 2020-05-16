<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
</Query>

void Main()
{
	var foo = new Foo { Xml = XDocument.Parse ("<test />") };

	IFormatter formatter = new BinaryFormatter();

	using (FileStream s = File.Create ("serialized.bin"))
		formatter.Serialize (s, foo);

	using (FileStream s = File.OpenRead ("serialized.bin"))
	{
		var f2 = (Foo)formatter.Deserialize (s);
		f2.Xml.Dump();
	}
}

[Serializable]
class Foo
{
	[NonSerialized]
	public XDocument Xml;

	string _xmlString;
	
	[OnSerializing] 
	void OnSerializing (StreamingContext context) => _xmlString = Xml.ToString();

	[OnDeserialized]
	void OnDeserialized (StreamingContext context) => Xml = XDocument.Parse (_xmlString);
}