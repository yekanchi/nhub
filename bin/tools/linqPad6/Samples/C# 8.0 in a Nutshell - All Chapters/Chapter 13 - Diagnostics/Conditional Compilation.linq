<Query Kind="Program" />

#define TESTMODE            // #define directives must be at top of file
#define PLAYMODE
//#undef PLAYMODE  // Cancels our define above if not commented out. Also cancels a define from the compiler e.g. through Visual Studio settings.
#define LOGGINGMODE

// Symbol names are uppercase by convention.

class Program
{
	static void Main()
	{
#if TESTMODE
    Console.WriteLine ("in test mode!"); 
#endif
#if TESTMODE && !PLAYMODE      // if TESTMODE and not PLAYMODE
    Console.WriteLine ("Test mode and NOT play mode"); 
#endif
#if PLAYMODE
    Console.WriteLine ("Play mode is defined.");
#endif
    LogStatus("Only if LOGGINGMODE is defined.");
	}

	[Conditional ("LOGGINGMODE")]
	static void LogStatus (string msg)
	{
		Console.WriteLine($"LOG: {msg}");
	}

}