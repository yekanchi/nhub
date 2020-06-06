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

int[] numbers  = { 1, 2, 3, 4, 5 };

numbers.FirstOrDefault (n => n > 10)
	.Dump ("The FirstOrDefault number > 10");

Customers.FirstOrDefault (c => c.Name == "Harry")
	.Dump ("First customer called 'Harry'");
	
Customers.FirstOrDefault (c => c.Purchases.Any (p => p.Price > 1000))
	.Dump ("First customer with a purchase > $1000");
	
Customers.FirstOrDefault (c => c.Name == "Dylan")
	.Dump ("First customer called 'Dylan', or default (null) if no match");