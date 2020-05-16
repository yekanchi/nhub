<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.IO.Compression</Namespace>
</Query>

void Main()
{
	AddFilesToFolder();
	
	var zipPath = Path.Combine(DirectoryToZip, "..", "archive.zip");
	ZipFile.CreateFromDirectory (DirectoryToZip, zipPath);
	
	Directory.CreateDirectory(DirectoryToExtractTo);
	ZipFile.ExtractToDirectory (zipPath, DirectoryToExtractTo);
	
	Console.WriteLine("Extracted files:");
	foreach (var file in Directory.EnumerateFiles(DirectoryToExtractTo))
		Console.WriteLine(file);
}

string DirectoryToZip
{
	get
	{
		return RuntimeInformation.IsOSPlatform (OSPlatform.Windows) ?
			@".\MyFolder" : "./MyFolder";
	}
}

string DirectoryToExtractTo
{
	get
	{
		return RuntimeInformation.IsOSPlatform (OSPlatform.Windows) ?
			@".\Extracted" : "./Extracted";
	}
}

void AddFilesToFolder()
{
	Directory.CreateDirectory (DirectoryToZip);
	foreach (var c in new char[] { 'A', 'B', 'C' })
		File.WriteAllText (Path.Combine (DirectoryToZip, $"{c}.txt"), $"This is {c}");
}