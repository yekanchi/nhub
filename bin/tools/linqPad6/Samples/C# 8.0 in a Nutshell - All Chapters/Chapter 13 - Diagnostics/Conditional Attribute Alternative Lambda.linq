<Query Kind="Program" />

class Program
{
	public static bool EnableLogging;
	
	static void Main()
	{
		EnableLogging = true;
		Func<string> msg1 = () => { Console.WriteLine ("The first lambda was evaluated"); return "My first message"; };
		LogStatus (msg1);

		EnableLogging = false;
		Func<string> msg2 = () => { Console.WriteLine ("The second lambda was evaluated"); return "My second message"; };
		LogStatus (msg2);
		
		Console.WriteLine("Let's see what was logged:");
		Console.WriteLine(File.ReadAllText("Conditional.log"));
	}

	static void LogStatus (Func<string> message)
	{
		string logFilePath = "Conditional.log";
		if (EnableLogging)
			System.IO.File.AppendAllText (logFilePath, message() + "\r\n");
	}
}