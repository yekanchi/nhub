<Query Kind="Statements">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

byte[] hash;

// Compute hash from file:

File.WriteAllText ("test.txt", @"
	The quick brown fox jumps over the lazy dog.
	The quick brown fox jumps over the lazy dog.
	The quick brown fox jumps over the lazy dog.");

using (Stream fs = File.OpenRead ("test.txt"))
	hash = SHA1.Create().ComputeHash (fs);   // SHA1 hash is 20 bytes long

hash.Dump ("Hash from test.txt");

// Compute hash from string:

byte[] data = System.Text.Encoding.UTF8.GetBytes ("stRhong%pword");
byte[] hash2 = SHA256.Create().ComputeHash (data);

hash2.Dump ("Hash from string");