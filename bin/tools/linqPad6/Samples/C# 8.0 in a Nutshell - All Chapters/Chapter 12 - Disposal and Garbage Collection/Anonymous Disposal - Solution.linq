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
		return Disposable.Create (() => _suspendCount--);
	}

	public void FireSomeEvent()
	{
		if (_suspendCount == 0) 
			"Event would fire".Dump();
		else
			"Event suppressed".Dump();
	}
}

// Reusable class
public class Disposable : IDisposable
{
	public static Disposable Create (Action onDispose)
		=> new Disposable (onDispose);

	Action _onDispose;
	Disposable (Action onDispose) => _onDispose = onDispose;

	public void Dispose()
	{
		_onDispose?.Invoke();
		_onDispose = null;
	}
}