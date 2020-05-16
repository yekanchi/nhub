<Query Kind="Statements">
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

try
{
	Purchases.Min ();
}
catch (Exception ex)
{
	ex.Message.Dump ("Purchases.Min()");
}

Purchases.Min (p => p.Price).Dump ("Lowest price");

Purchases
	.Where (p => p.Price == Purchases.Min (p2 => p2.Price))
	.FirstOrDefault()
	.Dump ("The cheapest purchase");