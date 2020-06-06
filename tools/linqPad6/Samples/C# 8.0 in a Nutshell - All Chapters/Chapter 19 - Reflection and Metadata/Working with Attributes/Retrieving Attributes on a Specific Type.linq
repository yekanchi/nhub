<Query Kind="Program" />

[Serializable, Obsolete]
class Test
{
	static void Main()
	{
		object[] atts = Attribute.GetCustomAttributes (typeof (Test));
		foreach (object att in atts) Console.WriteLine (att);
	}
}