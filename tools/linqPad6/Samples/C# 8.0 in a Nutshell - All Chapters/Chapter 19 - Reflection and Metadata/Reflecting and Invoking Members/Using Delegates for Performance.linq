<Query Kind="Program" />

delegate string StringToString (string s);

static void Main()
{
	MethodInfo trimMethod = typeof (string).GetMethod ("Trim", new Type[0]);

	// First let's test the performance when calling Invoke
	var sw = Stopwatch.StartNew();

	for (int i = 0; i < 1000000; i++)
		trimMethod.Invoke ("test", null);

	sw.Stop();
	sw.Dump ("Using Invoke");

	// Now let's test the performance when using a delegate:
	var trim = (StringToString) Delegate.CreateDelegate
                                      (typeof (StringToString), trimMethod);
	sw.Restart();
	
	for (int i = 0; i < 1000000; i++)
		trim ("test");
		
	sw.Stop();
	sw.Dump ("Using a delegate");
}