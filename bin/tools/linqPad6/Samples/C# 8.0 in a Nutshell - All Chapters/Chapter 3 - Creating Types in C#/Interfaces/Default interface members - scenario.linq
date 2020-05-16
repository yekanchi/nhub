<Query Kind="Program" />

void Main()
{
	ILogger foo = new Logger();
	foo.Log (new Exception ("test"));	
}

class Logger : ILogger
{	
	public void Log (string message) => Console.WriteLine (message);
}

interface ILogger
{
	// Let's suppose the interface as always defined this method:
	void Log (string message);	
	
	// Adding a new member to an interface need not break implementors:
	public void Log (Exception ex) => Log (ExceptionHeader + ex.Message);

	static string ExceptionHeader = "Exception: ";
}