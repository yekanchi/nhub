<Query Kind="Program">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Net.Sockets</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	RunServerAsync();

	using (TcpClient client = new TcpClient ("localhost", 51111))
	using (NetworkStream n = client.GetStream())
	{
		BinaryWriter w = new BinaryWriter (n);
		w.Write (Enumerable.Range (0, 5000).Select (x => (byte) x).ToArray());
		w.Flush();
		Console.WriteLine (new BinaryReader (n).ReadBytes (5000));
	}

}

async void RunServerAsync ()
{
	var listener = new TcpListener (IPAddress.Any, 51111);
	listener.Start ();
	try
	{
		while (true)
			Accept (await listener.AcceptTcpClientAsync ());
	}
	finally { listener.Stop(); }
}

async Task Accept (TcpClient client)
{
	await Task.Yield ();
	try
	{
		using (client)
		using (NetworkStream n = client.GetStream ())
		{
			byte[] data = new byte [5000];

			int bytesRead = 0; int chunkSize = 1;
			while (bytesRead < data.Length && chunkSize > 0)
				bytesRead += chunkSize =
					await n.ReadAsync (data, bytesRead, data.Length - bytesRead);

			Array.Reverse (data);   // Reverse the byte sequence
			await n.WriteAsync (data, 0, data.Length);
		}
	}
	catch (Exception ex) { Console.WriteLine (ex.Message); }
}