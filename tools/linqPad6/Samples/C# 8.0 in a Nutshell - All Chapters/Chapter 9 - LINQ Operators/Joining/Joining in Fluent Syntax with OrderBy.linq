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

var querySyntax =
	from c in Customers
	join p in Purchases on c.ID equals p.CustomerID
	orderby p.Price
	select c.Name + " bought a " + p.Description + " for $" + p.Price;
	
var fluentSyntax =
	Customers.Join (           // outer collection
		Purchases,               // inner collection
		c => c.ID,               // outer key selector
		p => p.CustomerID,       // inner key selector
		(c, p) => new { c, p }   // result selector 
	)
	.OrderBy (x => x.p.Price)
	.Select  (x => x.c.Name + " bought a " + x.p.Description + " for $" + x.p.Price);
	
querySyntax.Dump ("Query syntax");
fluentSyntax.Dump ("Same query in fluent syntax");