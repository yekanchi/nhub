<Query Kind="Program">
  <Namespace>System.IO.MemoryMappedFiles</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

void Main()
{
	var file = Path.Combine(TempDirectory, "interprocess.bin");
	File.WriteAllBytes (file, new byte [100]);
	
	using FileStream fs = new FileStream (file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
	using MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile (fs, null, fs.Length, MemoryMappedFileAccess.ReadWrite, HandleInheritability.None, true);
	using MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor();
		
	accessor.Write (0, 12345);
	
	// In LINQPad, manaully stop the query to exit
	Console.ReadLine();   // Keep shared memory alive until user hits Enter.
 
	File.Delete(file);
}

static string TempDirectory
{
	get => RuntimeInformation.IsOSPlatform (OSPlatform.Windows) ?
            @"C:\Temp" : "/tmp";
}