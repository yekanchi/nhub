<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

[DllImport ("kernel32.dll")]
static extern void GetSystemTime (SystemTime t);

static void Main()
{
	SystemTime t = new SystemTime();
	GetSystemTime (t);
	Console.WriteLine (t.Year);
}

[StructLayout (LayoutKind.Sequential)]
class SystemTime
{
	public ushort Year;
	public ushort Month;
	public ushort DayOfWeek;
	public ushort Day;
	public ushort Hour;
	public ushort Minute;
	public ushort Second;
	public ushort Milliseconds;
}