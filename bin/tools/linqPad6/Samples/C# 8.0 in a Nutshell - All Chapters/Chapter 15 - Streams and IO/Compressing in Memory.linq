<Query Kind="Statements">
  <Namespace>System.IO.Compression</Namespace>
</Query>

byte[] data = new byte [1000];          // We can expect a good compression
                                         // ratio from an empty array!
var ms = new MemoryStream();
using (Stream ds = new DeflateStream (ms, CompressionMode.Compress))
	ds.Write (data, 0, data.Length);

byte[] compressed = ms.ToArray();
Console.WriteLine (compressed.Length);       // 11

// Decompress back to the data array:
ms = new MemoryStream (compressed);
using (Stream ds = new DeflateStream (ms, CompressionMode.Decompress))
	for (int i = 0; i < 1000; i += ds.Read (data, i, 1000 - i)) ;