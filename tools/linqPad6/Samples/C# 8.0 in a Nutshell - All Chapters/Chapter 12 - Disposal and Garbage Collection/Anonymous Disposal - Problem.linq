<Query Kind="Program" />

void Main()
{
	var foo = new Foo();
	
	// Test it without calling SuspendEvents()
	foo.FireSomeEvent(); 
	
	// Now test it with event suspension:
	using (foo.SuspendEvents())
	{
		foo.FireSomeEvent();
	}

	// Now test it again without calling SuspendEvents()
	foo.FireSomeEvent();

}

class Foo
{
	int _suspendCount;

	public IDisposable SuspendEvents()
	{
		_suspendCount++;
		return new SuspendToken (this);
	}

	public void FireSomeEvent()
	{
		if (_suspendCount == 0)
			"Event would fire".Dump();
		else
			"Event suppressed".Dump();
	}

	class SuspendToken : IDisposable
	{
		Foo _foo;
		public SuspendToken (Foo foo) => _foo = foo;
		public void Dispose()
		{
			if (_foo != null) _foo._suspendCount--;
			_foo = null;
		}
	}
}