<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Buffers</Namespace>
</Query>

void Main()
{
	GetWindowsDirectory().Dump();
}

string GetWindowsDirectory()
{
	var array = ArrayPool<char>.Shared.Rent (256);
	try
	{
		int length = GetWindowsDirectory (array, 256);
		return new string (array, 0, length).ToString();
	}
	finally
	{
		ArrayPool<char>.Shared.Return (array);
	}
}

[DllImport ("kernel32.dll", CharSet = CharSet.Unicode)]
static extern int GetWindowsDirectory (char[] buffer, int maxChars);