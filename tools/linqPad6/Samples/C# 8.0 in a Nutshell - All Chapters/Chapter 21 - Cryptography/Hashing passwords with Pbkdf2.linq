<Query Kind="Statements">
  <NuGetReference>Microsoft.AspNetCore.Cryptography.KeyDerivation</NuGetReference>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>Microsoft.AspNetCore.Cryptography.KeyDerivation</Namespace>
</Query>

byte[] encrypted = KeyDerivation.Pbkdf2 (
    password: "stRhong%pword",
    salt: Encoding.UTF8.GetBytes ("j78Y#p)/saREN!y3@"),
    prf: KeyDerivationPrf.HMACSHA512,
    iterationCount: 100,
    numBytesRequested: 64);

encrypted.Dump();