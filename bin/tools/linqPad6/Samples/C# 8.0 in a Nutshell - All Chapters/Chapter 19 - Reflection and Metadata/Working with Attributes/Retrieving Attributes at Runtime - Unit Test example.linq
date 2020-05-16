<Query Kind="Program" />

void Main()
{
	foreach (MethodInfo mi in typeof (Foo).GetMethods())
	{
		TestAttribute att = (TestAttribute)Attribute.GetCustomAttribute
			(mi, typeof (TestAttribute));

		if (att != null)
			for (int i = 0; i < att.Repetitions; i++)
				try
				{
					mi.Invoke (new Foo(), null);    // Call method with no arguments
					$"Successfully called {mi.Name}".Dump();
				}
				catch (Exception ex)       // Wrap exception in att.FailureMessage
				{
					throw new Exception ("Error: " + att.FailureMessage, ex);
				}
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