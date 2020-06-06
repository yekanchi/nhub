<Query Kind="Statements">
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

using (var rsa = new RSACryptoServiceProvider())
{
	File.WriteAllText ("PublicKeyOnly.xml", rsa.ToXmlString (false));
	rsa.ToXmlString (false).Dump("PublicKeyOnly.xml");
	
	File.WriteAllText ("PublicPrivate.xml", rsa.ToXmlString (true));
	rsa.ToXmlString (true).Dump("PublicPrivate.xml");
}