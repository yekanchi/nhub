<Query Kind="Program" />

void Main()
{  
}

class Test : IDisposable
{
	public void Dispose()             // NOT virtual
	{
		Dispose (true);
		GC.SuppressFinalize (this);     // Prevent finalizer from running.
	}

	protected virtual void Dispose (bool disposing)
	{
		if (disposing)
		{
			// Call Dispose() on other objects owned by this instance.
			// You can reference other finalizable objects here.
			// ...
		}

		// Release unmanaged resources owned by (just) this object.
		// ...
	}

	~Test()
	{
		Dispose (false);
	}
}