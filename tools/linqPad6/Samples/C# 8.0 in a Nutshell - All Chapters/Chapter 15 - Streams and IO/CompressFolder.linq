<Query Kind="Program">
  <NuGetReference>System.Management</NuGetReference>
  <Namespace>System.Management</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

void Main()
{
	CreateCompressStructure();

	CompressFolder (DirectoryToCompress, true);
	
	CleanupCompressStructure();
}

static void CreateCompressStructure()
{
	Directory.CreateDirectory (DirectoryToCompress);
	File.WriteAllText (Path.Combine (DirectoryToCompress, "MyFile.txt"), "C# is fun!");
	var subfolder = Path.Combine (DirectoryToCompress, "Subfolder");
	Directory.CreateDirectory (subfolder);
	File.WriteAllText (Path.Combine (subfolder, "FileInSubfolder.txt"), ".NET Core rocks!");
}

static void CleanupCompressStructure()
{
	var subfolder = Path.Combine (DirectoryToCompress, "Subfolder");
	File.Delete(Path.Combine (subfolder, "FileInSubfolder.txt"));
	Directory.Delete(subfolder);
	
	File.Delete(Path.Combine (DirectoryToCompress, "MyFile.txt"));
	Directory.Delete(DirectoryToCompress);
}

static uint CompressFolder (string folder, bool recursive)
{
	string path = "Win32_Directory.Name='" + folder + "'";

	using (ManagementObject dir = new ManagementObject (path))
	using (ManagementBaseObject p = dir.GetMethodParameters ("CompressEx"))
	{
		p ["Recursive"] = recursive;
		using (ManagementBaseObject result = dir.InvokeMethod ("CompressEx",
                                                                 p, null))
			return (uint)result.Properties ["ReturnValue"].Value;
	}
}

static string DirectoryToCompress
{
	get
	{
		return RuntimeInformation.IsOSPlatform (OSPlatform.Windows) ?
			@"C:\Temp\CompressMe" : "/tmp/CompressMe"; // Requires a path outside LINQPad's current directory
	}
}