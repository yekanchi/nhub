<Query Kind="Program" />

// The is operator tests whether a reference conversion (or unboxing conversion) would succeed:

static void Main()
{
	Asset a = new Stock { SharesOwned = 3 };

	if (a is Stock s)
		Console.WriteLine (s.SharesOwned);
		
	// We can take this further:

	if (a is Stock s2 && s2.SharesOwned > 100000)
		Console.WriteLine ("Wealthy");
	else
		s2 = new Stock();   // s is in scope

	Console.WriteLine (s2.SharesOwned);  // Still in scope
}

public class Asset
{
	public string Name;
}

public class Stock : Asset   // inherits from Asset
{
	public long SharesOwned;
}

public class House : Asset   // inherits from Asset
{
	public decimal Mortgage;
}