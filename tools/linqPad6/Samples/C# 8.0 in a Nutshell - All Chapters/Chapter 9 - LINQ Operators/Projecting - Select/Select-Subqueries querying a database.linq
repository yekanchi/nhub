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

// Requires .ToList() because EF Core 3 cannot create Queryables in the select result.
// Punted for version 3 but may be resolved in the future. 
// Issue tracked at https://github.com/aspnet/EntityFrameworkCore/issues/16314

from c in Customers
select new
{
	c.Name,
	Purchases = (
		from p in Purchases
		where p.CustomerID == c.ID && p.Price > 1000
		select new { p.Description, p.Price }).ToList()
}