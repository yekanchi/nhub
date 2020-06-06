<Query Kind="Program">
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

[assembly: AssemblyFileVersion ("1.2.3.4")]

class Foo
{
	[field: NonSerialized]
	public int MyProperty { get; set; }
}

void Main()
{
}