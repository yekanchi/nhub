<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

class SupportsCompressionEncryption
{
	const int SupportsCompression = 0x10;
	const int SupportsEncryption = 0x20000;

	[DllImport ("Kernel32.dll", SetLastError = true)]
	extern static bool GetVolumeInformation (string vol, StringBuilder name,
		int nameSize, out uint serialNum, out uint maxNameLen, out uint flags,
		StringBuilder fileSysName, int fileSysNameSize);

	static void Main()
	{
		string volume = @"C:\";
		uint serialNum, maxNameLen, flags;
		bool ok = GetVolumeInformation (volume, null, 0, out serialNum,
                                        out maxNameLen, out flags, null, 0);
		if (!ok)
			throw new Win32Exception();

		bool canCompress = (flags & SupportsCompression) != 0;
		bool canEncrypt = (flags & SupportsEncryption) != 0;

		Console.WriteLine ($"Volume {volume} supports compression: {canCompress}; encryption: {canEncrypt}");
		
	}
}