<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
</Query>

void Main()
{
	Person p = new Person { Name = "George", DateOfBirth = new DateTime (1990, 1, 1) };

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
	public DateTime DateOfBirth;

	[NonSerialized] public int Age;
	[NonSerialized] public bool Valid = true;

	public Person() { Valid = true; }

	[OnDeserialized]
	void OnDeserialized (StreamingContext context)
	{
		TimeSpan ts = DateTime.Now - DateOfBirth;
		Age = ts.Days / 365;                         // Rough age in years
	}
}