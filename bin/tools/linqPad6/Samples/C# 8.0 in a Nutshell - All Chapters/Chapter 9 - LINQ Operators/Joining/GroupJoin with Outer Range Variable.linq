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

Customer[] customers = Customers.ToArray();
Purchase[] purchases = Purchases.ToArray();

var groupJoinQuery =
	from c in customers
	join p in purchases on c.ID equals p.CustomerID
	into custPurchases
	select new
	{
		CustName = c.Name,
		custPurchases
	};

var selectEquivalent =
	from c in customers
	select new
	{
		CustName = c.Name,
		custPurchases = purchases.Where (p => c.ID == p.CustomerID)
	};

@"The GroupJoin query is more efficient in this case, because we're querying
arrays (i.e. local collections).".Dump();
	
groupJoinQuery.Dump ("Group Join Query");
selectEquivalent.Dump ("Equivalent with Select");