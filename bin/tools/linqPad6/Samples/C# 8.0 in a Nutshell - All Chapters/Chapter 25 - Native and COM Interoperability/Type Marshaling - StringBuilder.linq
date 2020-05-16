<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	StringBuilder s = new StringBuilder (256);
	GetWindowsDirectory (s, 256);
	Console.WriteLine (s.ToString());
}

[DllImport ("kernel32.dll")]
static extern int GetWindowsDirectory (StringBuilder sb, int maxChars);