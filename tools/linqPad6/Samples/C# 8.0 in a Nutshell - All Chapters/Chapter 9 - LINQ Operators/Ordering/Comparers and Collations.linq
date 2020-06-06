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

string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

names.OrderBy (n => n, StringComparer.CurrentCultureIgnoreCase)
	.Dump ("Case insensitive ordering");

(
	from c in Customers
	orderby c.Name.ToUpper()
	select c.Name
)
.Dump ("Closest equivalent when querying a SQL Server database with default collation");