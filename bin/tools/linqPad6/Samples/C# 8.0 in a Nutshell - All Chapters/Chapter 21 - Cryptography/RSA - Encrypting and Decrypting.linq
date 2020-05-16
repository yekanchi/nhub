<Query Kind="Statements">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

// Create public/private keypair, and save to disk:

using (var rsa = new RSACryptoServiceProvider())
{
	File.WriteAllText ("PublicKeyOnly.xml", rsa.ToXmlString (false));
	File.WriteAllText ("PublicPrivate.xml", rsa.ToXmlString (true));
}

// Encrypt. Note that we can directly encrypt only small messages.

byte[] data = Encoding.UTF8.GetBytes ("Message to encrypt");

string publicKeyOnly = File.ReadAllText ("PublicKeyOnly.xml");
string publicPrivate = File.ReadAllText ("PublicPrivate.xml");

byte[] encrypted, decrypted;

using (var rsaPublicOnly = new RSACryptoServiceProvider())
{
	rsaPublicOnly.FromXmlString (publicKeyOnly);
	encrypted = rsaPublicOnly.Encrypt (data, true);

	// The next line would throw an exception because you need the private
	// key in order to decrypt:
	// decrypted = rsaPublicOnly.Decrypt (encrypted, true);
}

// Decrypt:

using (var rsaPublicPrivate = new RSACryptoServiceProvider())
{
	// With the private key we can successfully decrypt:
	rsaPublicPrivate.FromXmlString (publicPrivate);
	decrypted = rsaPublicPrivate.Decrypt (encrypted, true);
	
	Encoding.UTF8.GetString (decrypted).Dump ("decrypted");
}