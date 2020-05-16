<Query Kind="Program" />

void Main()
{
	foreach (MethodInfo mi in typeof (Foo).GetMethods())
	{
		TestAttribute att = (TestAttribute)Attribute.GetCustomAttribute
			(mi, typeof (TestAttribute));

		if (att != null)
			Console.WriteLine ("Method {0} will be tested; reps={1}; msg={2}",
                                mi.Name, att.Repetitions, att.FailureMessage);
	}
}

[AttributeUsage (AttributeTargets.Method)]
public sealed class TestAttribute : Attribute
{
	public int Repetitions;
	public string FailureMessage;

	public TestAttribute () : this (1) { }
	public TestAttribute (int repetitions) { Repetitions = repetitions; }
}

class Foo
{
	[Test]
	public void Method1() { }

	[Test (20)]
	public void Method2() { }

	[Test (20, FailureMessage = "Debugging Time!")]
	public void Method3() { }
}