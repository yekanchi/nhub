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
	FilterSortProducts (Products).Dump ("This query executes on SQL Server");
	
	Product[] localProducts =
	{
		new Product { ID = 1, Description = "Local Product Test", LastSale = new DateTime (2007, 2, 3) }
	};
	
	FilterSortProducts (localProducts.AsQueryable()).Dump ("The same query - executing locally");
}

IQueryable<Product> FilterSortProducts (IQueryable<Product> input)
{
	return 
		from p in input
		where !p.Discontinued && p.LastSale < DateTime.Now.AddDays (-7)
		orderby p.Description
		select p;
}