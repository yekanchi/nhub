<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Runtime.Loader</Namespace>
</Query>

void Main()
{
	FolderBasedALC alc = new FolderBasedALC (@"C:\YourAssemblyFolder");
	alc.LoadFromAssemblyName (new AssemblyName ("YourAssembly"));

	foreach (Assembly a in alc.Assemblies)
		Console.WriteLine (a.FullName);
}

class FolderBasedALC : AssemblyLoadContext
{
	readonly string _folder;
	public FolderBasedALC (string folder) => _folder = folder;

	protected override Assembly Load (AssemblyName assemblyName)
	{
		// Attempt to find the assembly:
		string targetPath = Path.Combine (_folder, assemblyName.Name + ".dll");

		if (File.Exists (targetPath))
			return LoadFromAssemblyPath (targetPath);   // Load the assembly

		return null;    // We can’t find it – it could be a framework assembly
	}
}