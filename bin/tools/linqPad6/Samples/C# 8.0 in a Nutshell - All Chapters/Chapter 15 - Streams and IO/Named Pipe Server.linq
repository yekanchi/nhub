<Query Kind="Statements">
  <Namespace>System.IO.Pipes</Namespace>
</Query>

bool messageMode = true; // Be sure Server and Client set the same

if (messageMode)
{
	using var s = new NamedPipeServerStream ("pipedream", PipeDirection.InOut,
                                              1, PipeTransmissionMode.Message);

	s.WaitForConnection();

	byte[] msg = Encoding.UTF8.GetBytes ("Hello");
	s.Write (msg, 0, msg.Length);

	Console.WriteLine (Encoding.UTF8.GetString (ReadMessage (s)));
}
else
{
	using var s = new NamedPipeServerStream ("pipedream");

	Console.WriteLine ("Please start Named Pipe Client.");
	s.WaitForConnection();
	s.WriteByte (100);                // Send the value 100.
	Console.WriteLine ("Response from Named Pipe Client.");
	Console.WriteLine (s.ReadByte());
}

static byte[] ReadMessage (PipeStream s)
{
	MemoryStream ms = new MemoryStream();
	byte[] buffer = new byte [0x1000];      // Read in 4 KB blocks

	do { ms.Write (buffer, 0, s.Read (buffer, 0, buffer.Length)); }
	while (!s.IsMessageComplete);

	return ms.ToArray();
}