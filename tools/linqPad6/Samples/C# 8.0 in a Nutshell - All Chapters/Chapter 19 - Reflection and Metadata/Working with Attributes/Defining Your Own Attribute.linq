<Query Kind="Program" />

void Main()
{  
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