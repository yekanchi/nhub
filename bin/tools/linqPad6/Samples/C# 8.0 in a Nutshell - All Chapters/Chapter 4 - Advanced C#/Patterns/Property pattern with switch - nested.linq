<Query Kind="Program" />

void Main()
{
	Console.WriteLine (ShouldAllow (new Uri ("http://www.linqpad.net")));
	Console.WriteLine (ShouldAllow (new Uri ("ftp://ftp.microsoft.com")));
	Console.WriteLine (ShouldAllow (new Uri ("tcp:foo.database.windows.net")));
}

bool ShouldAllow (Uri uri) => uri switch
{
	{ Scheme: string { Length: 4 }, Port: 80 } => true,
	_ => false
};