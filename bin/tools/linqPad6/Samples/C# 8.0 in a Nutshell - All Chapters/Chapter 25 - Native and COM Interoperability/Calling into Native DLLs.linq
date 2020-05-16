<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

void Main()
{
	MessageBox (IntPtr.Zero,
                "Please do not press this again.", "Attention", 0);
}

[DllImport ("user32.dll")]
static extern int MessageBox (IntPtr hWnd, string text, string caption, int type);