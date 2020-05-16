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

Customer[] customers = Customers.ToArray();
Purchase[] purchases = Purchases.ToArray();

var slowQuery =
	from c in customers
	from p in purchases where c.ID == p.CustomerID
	select c.Name + " bought a " + p.Description;

var fastQuery =
	from c in customers
	join p in purchases on c.ID equals p.CustomerID
	select c.Name + " bought a " + p.Description;
								
slowQuery.Dump ("Slow local query with SelectMany");                
fastQuery.Dump ("Fast local query with Join");