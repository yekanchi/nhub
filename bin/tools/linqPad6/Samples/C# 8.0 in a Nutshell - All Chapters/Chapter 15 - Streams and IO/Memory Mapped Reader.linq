<Query Kind="Program">
  <Namespace>System.IO.MemoryMappedFiles</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

void Main()
{
	// This can run in a separate executable:
	var file = Path.Combine (TempDirectory, "interprocess.bin");
	using FileStream fs = new FileStream (file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
	using MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile (fs, null, fs.Length, MemoryMappedFileAccess.ReadWrite, HandleInheritability.None, true);
	using MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor();
	
	Console.WriteLine (accessor.ReadInt32 (0));   // 12345
}

static string TempDirectory
{
	get => RuntimeInformation.IsOSPlatform (OSPlatform.Windows) ?
            @"C:\Temp" : "/tmp";
}