<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main() => EnumWindows (PrintWindow, IntPtr.Zero);

delegate bool EnumWindowsCallback (IntPtr hWnd, IntPtr lParam);

[DllImport ("user32.dll")]
static extern int EnumWindows (EnumWindowsCallback hWnd, IntPtr lParam);

static bool PrintWindow (IntPtr hWnd, IntPtr lParam)
{
	Console.WriteLine (hWnd.ToInt64());
	return true;
}