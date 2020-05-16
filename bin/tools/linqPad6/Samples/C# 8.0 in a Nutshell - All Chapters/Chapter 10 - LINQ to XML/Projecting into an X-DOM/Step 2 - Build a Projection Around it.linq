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

var customers =
	new XElement ("customers",
		// The AsEnumerable call can be removed when the EF Core bug is fixed.
		from c in Customers.AsEnumerable()
		select 
			new XElement ("customer", new XAttribute ("id", c.ID),
				new XElement ("name", c.Name),
				new XElement ("buys", c.Purchases.Count)
			)
		);
	
customers.Dump();