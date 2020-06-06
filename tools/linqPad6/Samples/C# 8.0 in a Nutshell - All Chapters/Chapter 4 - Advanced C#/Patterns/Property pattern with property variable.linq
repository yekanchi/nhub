<Query Kind="Program" />

void Main()
{
	Console.WriteLine (ShouldAllow (new Uri ("http://www.linqpad.net")));
	Console.WriteLine (ShouldAllow (new Uri ("ftp://ftp.microsoft.com")));
	Console.WriteLine (ShouldAllow (new Uri ("tcp:foo.database.windows.net")));
}

bool ShouldAllow (Uri uri) => uri switch
{
	{ Scheme: "http", Port: 80, Host: var host } => host.Length < 1000,
	{ Scheme: "https", Port: 443 } => true,
	{ Scheme: "ftp", Port: 21 } => true,
	{ IsLoopback: true } => true,
	_ => false
};