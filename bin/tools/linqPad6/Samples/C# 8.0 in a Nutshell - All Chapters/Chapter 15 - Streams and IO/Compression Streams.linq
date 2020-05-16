<Query Kind="Statements">
  <Namespace>System.IO.Compression</Namespace>
</Query>

using (Stream s = File.Create ("compressed.bin"))
using (Stream ds = new DeflateStream (s, CompressionMode.Compress))
	for (byte i = 0; i < 100; i++)
		ds.WriteByte (i);
		
new FileInfo ("compressed.bin").Length.Dump ("Length of compressed file");

using (Stream s = File.OpenRead ("compressed.bin"))
using (Stream ds = new DeflateStream (s, CompressionMode.Decompress))
	for (byte i = 0; i < 100; i++)
		Console.WriteLine (ds.ReadByte());     // Writes 0 to 99