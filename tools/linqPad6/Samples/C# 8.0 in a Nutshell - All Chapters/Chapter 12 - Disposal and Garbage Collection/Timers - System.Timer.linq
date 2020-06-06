<Query Kind="Program">
  <Namespace>System.Timers</Namespace>
</Query>

void Main()
{
	
}

class Foo : IDisposable
{
	System.Timers.Timer _timer;

	Foo()
	{
		_timer = new System.Timers.Timer { Interval = 1000 };
		_timer.Elapsed += tmr_Elapsed;
		_timer.Start();
	}

	void tmr_Elapsed (object sender, ElapsedEventArgs e) {  }
	
	public void Dispose() { _timer.Dispose(); }
}