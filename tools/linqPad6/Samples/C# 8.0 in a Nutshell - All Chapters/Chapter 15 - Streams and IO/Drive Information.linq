<Query Kind="Statements" />

DriveInfo c = new DriveInfo ("C");       // Query the C: drive.

long totalSize = c.TotalSize;            // Size in bytes.
long freeBytes = c.TotalFreeSpace;       // Ignores disk quotas.
long freeToMe = c.AvailableFreeSpace;   // Takes quotas into account.

foreach (DriveInfo d in DriveInfo.GetDrives().OrderBy(d => d.Name))    // All defined drives.
{
	Console.WriteLine (d.Name);             // C:\
	Console.WriteLine (d.DriveType);        // Fixed
	Console.WriteLine (d.RootDirectory);    // C:\

	if (d.IsReady)   // If the drive is not ready, the following two
                     // properties will throw exceptions:
	{
		Console.WriteLine (d.VolumeLabel);    // The Sea Drive
		Console.WriteLine (d.DriveFormat);    // NTFS
	}
	Console.WriteLine();
}