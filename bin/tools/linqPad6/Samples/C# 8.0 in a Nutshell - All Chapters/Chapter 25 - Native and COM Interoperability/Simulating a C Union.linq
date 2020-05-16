<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	NoteMessage n = new NoteMessage();
	Console.WriteLine (n.PackedMsg);    // 0

	n.Channel = 10;
	n.Note = 100;
	n.Velocity = 50;
	Console.WriteLine (n.PackedMsg);    // 3302410

	n.PackedMsg = 3328010;
	Console.WriteLine (n.Note);         // 200
}

[DllImport ("winmm.dll")]
public static extern uint midiOutShortMsg (IntPtr handle, uint message);

[StructLayout (LayoutKind.Explicit)]
public struct NoteMessage
{
	[FieldOffset (0)] public uint PackedMsg;    // 4 bytes long

	[FieldOffset (0)] public byte Channel;      // FieldOffset also at 0
	[FieldOffset (1)] public byte Note;
	[FieldOffset (2)] public byte Velocity;
}