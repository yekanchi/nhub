<Query Kind="Statements">
  <Namespace>System.Net</Namespace>
</Query>

foreach (IPAddress a in Dns.GetHostAddresses ("albahari.com"))
	Console.WriteLine (a.ToString());     // 205.210.42.167

IPHostEntry entry = Dns.GetHostEntry ("205.210.42.167");
Console.WriteLine (entry.HostName);                    // albahari.com

IPAddress address = new IPAddress (new byte[] { 205, 210, 42, 167 });
IPHostEntry entry2 = Dns.GetHostEntry (address);
Console.WriteLine (entry2.HostName);                    // albahari.com

foreach (IPAddress a in await Dns.GetHostAddressesAsync ("albahari.com"))
	Console.WriteLine (a.ToString());