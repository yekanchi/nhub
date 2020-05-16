<Query Kind="Program" />

void Main()
{
	MemberInfo[] members = typeof (Walnut).GetMembers();
	foreach (MemberInfo m in members)
		Console.WriteLine (m);
}

class Walnut
{
	private bool cracked;
	public void Crack() { cracked = true; }
}