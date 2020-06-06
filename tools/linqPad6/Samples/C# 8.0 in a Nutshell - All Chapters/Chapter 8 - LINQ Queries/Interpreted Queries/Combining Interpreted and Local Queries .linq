<Query Kind="Program">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c394</ID>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFile>true</AttachFile>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <DisplayName>Nutshell</DisplayName>
    <Persist>true</Persist>
    <DriverData>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	// This uses a custom 'Pair' extension method, defined below.
	
	Customers
		.Select (c => c.Name.ToUpper())
		.Pair()									// Local from this point on.
		.OrderBy (n => n)
		.Dump();	

	// Here's a more substantial example:

	Customers
		.Select (c => c.Name.ToUpper())
		.OrderBy (n => n)
		.Pair()                         // Local from this point on.
		.Select ((n, i) => "Pair " + i.ToString() + " = " + n)
		.Dump();	
}

public static class MyExtensions
{
	public static IEnumerable<string> Pair (this IEnumerable<string> source)
	{
		string firstHalf = null;
		foreach (string element in source)
		if (firstHalf == null)
			firstHalf = element;
		else
		{
			yield return firstHalf + ", " + element;
			firstHalf = null;
		}
	}
}