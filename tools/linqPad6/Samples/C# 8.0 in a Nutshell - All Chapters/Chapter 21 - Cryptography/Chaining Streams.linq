<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.IO.Compression</Namespace>
</Query>

byte[] key = new byte [16];
byte[] iv = new byte [16];

var cryptoRng = RandomNumberGenerator.Create();
cryptoRng.GetBytes (key);
cryptoRng.GetBytes (iv);

using (Aes algorithm = Aes.Create())
{
	using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
	using (Stream f = File.Create ("serious.bin"))
	using (Stream c = new CryptoStream (f, encryptor, CryptoStreamMode.Write))
	using (Stream d = new DeflateStream (c, CompressionMode.Compress))
	using (StreamWriter w = new StreamWriter (d))
		await w.WriteLineAsync ("Small and secure!");

	using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
	using (Stream f = File.OpenRead ("serious.bin"))
	using (Stream c = new CryptoStream (f, decryptor, CryptoStreamMode.Read))
	using (Stream d = new DeflateStream (c, CompressionMode.Decompress))
	using (StreamReader r = new StreamReader (d))
		Console.WriteLine (await r.ReadLineAsync());     // Small and secure!
}