<Query Kind="Statements">
  <Namespace>System.IO.Compression</Namespace>
</Query>

string[] words = "The quick brown fox jumps over the lazy dog".Split();
Random rand = new Random(0);

using (Stream s = File.Create ("compressed.bin"))
using (Stream ds = new BrotliStream (s, CompressionMode.Compress))
using (TextWriter w = new StreamWriter (ds))
	for (int i = 0; i < 1000; i++)
		await w.WriteAsync (words [rand.Next (words.Length)] + " ");

new FileInfo ("compressed.bin").Length.Dump ("Length of compressed file");

using (Stream s = File.OpenRead ("compressed.bin"))
using (Stream ds = new BrotliStream (s, CompressionMode.Decompress))
using (TextReader r = new StreamReader (ds))
	Console.Write (await r.ReadToEndAsync());  // Output below: