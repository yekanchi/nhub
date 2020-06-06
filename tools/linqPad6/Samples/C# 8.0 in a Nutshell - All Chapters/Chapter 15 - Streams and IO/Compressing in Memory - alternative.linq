<Query Kind="Statements">
  <Namespace>System.IO.Compression</Namespace>
</Query>

byte[] data = new byte[1000];

MemoryStream ms = new MemoryStream();
using (Stream ds = new DeflateStream (ms, CompressionMode.Compress, true))
	await ds.WriteAsync (data, 0, data.Length);

Console.WriteLine (ms.Length);             // 113
ms.Position = 0;
using (Stream ds = new DeflateStream (ms, CompressionMode.Decompress))
	for (int i = 0; i < 1000; i += await ds.ReadAsync (data, i, 1000 - i)) ;