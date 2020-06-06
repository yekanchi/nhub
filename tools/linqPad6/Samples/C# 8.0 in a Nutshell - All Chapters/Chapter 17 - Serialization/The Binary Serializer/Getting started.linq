<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
</Query>

void Main()
{
	Person p = new Person { Name = "George", Age = 25 };

	IFormatter formatter = new BinaryFormatter();

	using (FileStream s = File.Create ("serialized.bin"))
		formatter.Serialize (s, p);
		
	using (FileStream s = File.OpenRead ("serialized.bin"))
	{
		Person p2 = (Person)formatter.Deserialize (s);
		Console.WriteLine (p2.Name + " " + p2.Age);     // George 25
	}
}

[Serializable]
public sealed class Person
{
	public string Name;
	public int Age;
}