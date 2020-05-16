<Query Kind="Program" />

#define TRACE

void Main()
{
	// This will NOT run within LINQPad but will run in a program
	
	Trace.Listeners.Clear();
	
	// Add a writer that appends to the trace.txt file:
	var fileListener = new TextWriterTraceListener("trace.txt");
	Trace.Listeners.Add(fileListener);
	
	// Obtain the Console's output stream, then add that as a listener:
	System.IO.TextWriter tw = Console.Out;
	Trace.Listeners.Add(new TextWriterTraceListener(tw));
	
	// Set up a Windows Event log source and then create/add listener.
	// CreateEventSource requires administrative elevation, so this would
	// typically be done in application setup.
	
	/* The following requires Administrator permission to run and is Windows-specific
	if (!EventLog.SourceExists ("DemoApp"))
		EventLog.CreateEventSource ("DemoApp", "Application");
	
	Trace.Listeners.Add (new EventLogTraceListener ("DemoApp"));
	*/
	
	Console.WriteLine("Writing to trace. Will appear on console and in trace.txt.");
	
	Trace.WriteLine("Foo");
	
	bool myCondition = true;
	Trace.WriteLineIf (myCondition, "This will write");
	Trace.WriteLineIf (!myCondition, "This will NOT write");
	
	Console.WriteLine ("Done writing to trace. Let's see what's in trace.txt:");
	fileListener.Close();
	Console.WriteLine (File.ReadAllText ("trace.txt"));
	
}

// Define other methods, classes and namespaces here