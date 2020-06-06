<Query Kind="Program" />

void Main()
{
	ILogger.Prefix = "File log: ";

	var logger = new Logger();  
	((ILogger)logger).Log ("message");  
}

interface ILogger
{
	void Log (string text) =>
		Console.WriteLine (Prefix + text);

	static string Prefix = "";
}


class Logger : ILogger
{
	// We don't need to implement anything
}