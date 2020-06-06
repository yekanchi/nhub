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

var query1 =
	from c in Customers
	from p in Purchases
	where c.ID == p.CustomerID
	select c.Name + " bought a " + p.Description;

var query2 =
	from c in Customers
	from p in c.Purchases
	select c.Name + " bought a " + p.Description;

query1.Dump ("Using SelectMany to manually join");
query2.Dump ("Using an Association to achieve the same result");