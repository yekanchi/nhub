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

from c in Customers
from p in c.Purchases.Where (p => p.Price > 1000).DefaultIfEmpty()
select new
{
	c.Name,
	p.Description,
	Price = (decimal?)p.Price
}

// We could do this instead:

//var dbQuery =
//from c in Customers
//select new
//{
//  c.Name,
//  Purchases =
//    from p in c.Purchases
//    where p.Price > 1000
//    select new { p.Description, p.Price }
//};
//
//dbQuery.Dump();

// It achieves the same result (outer join), except that you end up with shaped data 
// instead of flat data. Shaped data is usually easier to work with, anyway, and
// if you really need flat data, you could always flatten it via a local query at
// the end:

//var flat =
//  from result in dbQuery.AsEnumerable()
//  from purchase in result.Purchases.DefaultIfEmpty()
//  select new
//  {
//  	result.Name,
//  	purchase?.Description,
//  	purchase?.Price
//  };
//  
//flat.Dump ("If you really need flat data");

// Yet another option is to check out whether the joining operators can be
// used to achieve this result in EFCore.