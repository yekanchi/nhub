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

// The following groups purchases by year, then returns only those groups where
// the average purchase across the year was greater than $1000:

from p in Purchases
group p.Price by p.Date.Year into salesByYear
where salesByYear.Average (x => x) > 1000
select new
{
	Year       = salesByYear.Key,
	TotalSales = salesByYear.Count(),
	AvgSale    = salesByYear.Average(),
	TotalValue = salesByYear.Sum()
}