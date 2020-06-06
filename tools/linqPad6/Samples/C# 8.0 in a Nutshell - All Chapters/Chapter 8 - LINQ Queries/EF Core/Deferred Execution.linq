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

var query = from c in Customers
            select
            	 from p in c.Purchases
            	 select new { c.Name, p.Price };

foreach (var customerPurchaseResults in query)
	foreach (var namePrice in customerPurchaseResults)
		Console.WriteLine ($"{ namePrice.Name} spent { namePrice.Price}");

var query2 = from c in Customers
             select new { c.Name, c.Purchases };

foreach (var row in query2)
	foreach (Purchase p in row.Purchases)   // No extra round-tripping
		Console.WriteLine (row.Name + " spent " + p.Price);