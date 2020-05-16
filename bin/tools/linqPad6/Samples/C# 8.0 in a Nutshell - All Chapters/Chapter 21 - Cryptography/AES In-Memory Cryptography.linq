<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	byte[] key = new byte[16];
	byte[] iv = new byte[16];

	var cryptoRng = RandomNumberGenerator.Create();
	cryptoRng.GetBytes (key);
	cryptoRng.GetBytes (iv);
	
	Console.WriteLine ($"Key: {string.Join ("", key.Select (b => b.ToString ("x2")))}");
	Console.WriteLine ($"IV : {string.Join ("", iv.Select (b => b.ToString ("x2")))}");
	
	string toEncrypt = "There are 10 kinds of people. Those that understand binary, and those that don't.";
	byte[] encrypted = MemCrypt.Encrypt(Encoding.UTF8.GetBytes(toEncrypt), key, iv);

	Console.WriteLine ($"Encrypted: {string.Join ("", encrypted.Select (b => b.ToString ("x2")))}");
	
	byte[] decrypted = MemCrypt.Decrypt(encrypted, key, iv);
	Console.WriteLine ($"Decrypted: {Encoding.UTF8.GetString(decrypted)}");

	string encrypted2 = MemCrypt.Encrypt ("Yeah!", key, iv);
	Console.WriteLine (encrypted2);                 // R1/5gYvcxyR2vzPjnT7yaQ==

	string decrypted2 = MemCrypt.Decrypt (encrypted2, key, iv);
	Console.WriteLine (decrypted2);                 // Yeah!

	Array.Clear (key, 0, key.Length);
	Array.Clear (iv, 0, iv.Length);
	
	Console.WriteLine ($"Key: {string.Join ("", key.Select (b => b.ToString ("x2")))}");
	Console.WriteLine ($"IV : {string.Join ("", iv.Select (b => b.ToString ("x2")))}");

}

class MemCrypt
{
	public static byte[] Encrypt (byte[] data, byte[] key, byte[] iv)
	{
		using (Aes algorithm = Aes.Create())
		using (ICryptoTransform encryptor = algorithm.CreateEncryptor (key, iv))
			return Crypt (data, encryptor);
	}

	public static byte[] Decrypt (byte[] data, byte[] key, byte[] iv)
	{
		using (Aes algorithm = Aes.Create())
		using (ICryptoTransform decryptor = algorithm.CreateDecryptor (key, iv))
			return Crypt (data, decryptor);
	}

	public static string Encrypt (string data, byte[] key, byte[] iv)
	{
		return Convert.ToBase64String (
			Encrypt (Encoding.UTF8.GetBytes (data), key, iv));
	}

	public static string Decrypt (string data, byte[] key, byte[] iv)
	{
		return Encoding.UTF8.GetString (
			Decrypt (Convert.FromBase64String (data), key, iv));
	}

	static byte[] Crypt (byte[] data, ICryptoTransform cryptor)
	{
		MemoryStream m = new MemoryStream();
		using (Stream c = new CryptoStream (m, cryptor, CryptoStreamMode.Write))
			c.Write (data, 0, data.Length);
		return m.ToArray();
	}
}