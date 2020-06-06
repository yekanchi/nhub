<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

byte[] data = { 1, 2, 3, 4, 5 };   // This is what we're encrypting.

using (var rsa = new RSACryptoServiceProvider())
{
	rsa.KeySize.Dump();
	byte[] encrypted = rsa.Encrypt (data, true);
	byte[] decrypted = rsa.Decrypt (encrypted, true);
}