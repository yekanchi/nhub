<Query Kind="Program">
  <Namespace>System.Collections.Concurrent</Namespace>
</Query>

string filename = "tempref.tmp";

void Main()
{
	// Create and open file so it cannot be deleted
	var writer = File.CreateText(filename);
	
	// Get the temporary reference in a separate method.
	// Variable will go out of scope upon return and be eligible for GC.
	CreateTempFileRef();
	
	GC.Collect(); // Run the garbage collector
	
	TempFileRef._failedDeletions.Dump();
}

void CreateTempFileRef()
{
	var tempRef = new TempFileRef(filename); 
	
}

public class TempFileRef
{
	static internal ConcurrentQueue<TempFileRef> _failedDeletions
		= new ConcurrentQueue<TempFileRef>();

	public readonly string FilePath;
	public Exception DeletionError { get; private set; }

	public TempFileRef (string filePath) { FilePath = filePath; }

	// int _deleteAttempt; // Uncomment if re-registering the finalizer
	
	~TempFileRef()
	{
		try { File.Delete (FilePath); }
		catch (Exception ex)
		{
			DeletionError = ex;
			_failedDeletions.Enqueue (this);   // Resurrection
			// We can re-register for finalization by uncommenting:
			// if (_deleteAttempt++ < 3) GC.ReRegisterForFinalize (this);
		}
	}
}