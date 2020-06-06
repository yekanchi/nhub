<Query Kind="Statements">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

byte[] original = { 1, 2, 3, 4, 5 };
original.Dump ("Original");

DataProtectionScope scope = DataProtectionScope.CurrentUser;

byte[] encrypted = ProtectedData.Protect (original, null, scope);
encrypted.Dump("Encrypted");

byte[] decrypted = ProtectedData.Unprotect (encrypted, null, scope);
// decrypted is now {1, 2, 3, 4, 5}
decrypted.Dump("Decrypted");