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

(
	from c in Customers
	from p in c.Purchases
	where p.Price > 1000
	select new { c.Name, p.Description, p.Price }
)
.Dump ("An inner join");

(
	from c in Customers
	from p in c.Purchases.DefaultIfEmpty()
	select new { c.Name, p.Description, Price = (decimal?) p.Price }
)
.Dump ("An outer join (without the predicate)");