<Query Kind="Statements" />

Console.WriteLine("LOGS ON THIS COMPUTER");
foreach (EventLog l in EventLog.GetEventLogs())
{
	try
	{
		Console.WriteLine (l.LogDisplayName);
	}
	catch (Exception ex)
	{
		// The display name might be unavailable but this property still is available
		Console.WriteLine ($"Error processing an event log '{l.Log}': {ex.Message}");
	}
}

Console.WriteLine();
Console.WriteLine ("Appliation EVENT LOG");

EventLog log = new EventLog ("Application");

Console.WriteLine ("Total entries: " + log.Entries.Count);

EventLogEntry last = log.Entries [log.Entries.Count - 1];
Console.WriteLine ("Index:   " + last.Index);
Console.WriteLine ("Source:  " + last.Source);
Console.WriteLine ("Type:    " + last.EntryType);
Console.WriteLine ("Time:    " + last.TimeWritten);
Console.WriteLine ("Message: " + last.Message);

log.EnableRaisingEvents = true;
log.EntryWritten += DisplayEntry;

// Monitor for event log entries for 60 seconds
// If running in LINQPad, you can stop the query to end early
Thread.Sleep(60 * 1000); 

static void DisplayEntry (object sender, EntryWrittenEventArgs e)
{
	EventLogEntry entry = e.Entry;
	Console.WriteLine (entry.Message);
}