<Query Kind="Program" />

static readonly object _locker = new object();
static int _val1, _val2;

static void Main()
{
	for (int i = 1; i <= 1000; i++)
	{
		if (i % 100 == 0) Console.WriteLine ($"Tried {i} times to get DivideByZeroException");
		
		var t1 = new Thread (Go); t1.Start();
		var t2 = new Thread (Go); t2.Start();
		var t3 = new Thread (Go); t3.Start();
		
		t1.Join(); t2.Join(); t3.Join();
	}
}

static void Go()
{
	lock (_locker)	// Threadsafe: will never get DivideByZeroException
	{
		if (_val2 != 0) Console.WriteLine (_val1 / _val2);
		_val2 = 0;
	}
}