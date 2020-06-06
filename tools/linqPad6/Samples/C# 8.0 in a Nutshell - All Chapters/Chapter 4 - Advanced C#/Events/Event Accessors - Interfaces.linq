<Query Kind="Program" />

// When explicitly implementing an interface that declares an event, you must use event accessors:

public interface IFoo { event EventHandler Ev; }

class Foo : IFoo
{
	private EventHandler ev;
	
	event EventHandler IFoo.Ev
	{
		add    { ev += value; }
		remove { ev -= value; }
	}
}

static void Main() { }