<Query Kind="Expression">
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

// The let keyword in query expressions comes in useful with subqueries: it lets
// you re-use the subquery in the projection:

from c in Customers
let highValuePurchases = c.Purchases.Where (p => p.Price > 1000)
where highValuePurchases.Any()
select new
{
	c.Name,
	highValuePurchases
}

// We'll see more examples of this in the following section, "Projecting".