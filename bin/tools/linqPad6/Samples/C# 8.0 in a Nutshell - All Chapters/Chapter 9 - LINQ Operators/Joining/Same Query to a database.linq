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

// Requires .ToList() because EF Core 3 cannot create Queryables in the select result.
// Punted for version 3 but may be resolved in the future. 
// Issue tracked at https://github.com/aspnet/EntityFrameworkCore/issues/16314

var groupJoinQuery =
	from c in Customers.AsEnumerable()
	join p in Purchases.AsEnumerable() on c.ID equals p.CustomerID
	into custPurchases
	select new
	{
		CustName = c.Name,
		custPurchases
	};

var selectEquivalent =
	from c in Customers 
	select new
	{
		CustName = c.Name,
		custPurchases = Purchases.Where (p => c.ID == p.CustomerID).ToList()
	};

@"Notice in the SQL results pane, that there's no difference between these two queries.
The second query, however, is more flexibile.".Dump();	

groupJoinQuery.Dump ("Group Join Query");
selectEquivalent.Dump ("Equivalent with Select");