<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
</Query>

IPAddress a1 = new IPAddress (new byte[] { 101, 102, 103, 104 });
IPAddress a2 = IPAddress.Parse ("101.102.103.104");
Console.WriteLine (a1.Equals (a2));                     // True
Console.WriteLine (a1.AddressFamily);                   // InterNetwork

IPAddress a3 = IPAddress.Parse
	("[3EA0:FFFF:198A:E4A3:4FF2:54fA:41BC:8D31]");
Console.WriteLine (a3.AddressFamily);   // InterNetworkV6

Console.WriteLine();
Console.WriteLine("Address with port:");
IPAddress a = IPAddress.Parse ("101.102.103.104");
IPEndPoint ep = new IPEndPoint (a, 222);           // Port 222
Console.WriteLine (ep.ToString());                 // 101.102.103.104:222