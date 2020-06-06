<Query Kind="Program" />

// When a delegate object is assigned to an instance method, the delegate object must maintain
// a reference not only to the method, but also to the instance to which the method belongs:

public delegate void ProgressReporter (int percentComplete);

static void Main()
{
	X x = new X();
	ProgressReporter p = x.InstanceProgress;
	p(99);                                 // 99
	Console.WriteLine (p.Target == x);     // True
	Console.WriteLine (p.Method);          // Void InstanceProgress(Int32)
}

class X
{
	public void InstanceProgress (int percentComplete) => Console.WriteLine (percentComplete);
}