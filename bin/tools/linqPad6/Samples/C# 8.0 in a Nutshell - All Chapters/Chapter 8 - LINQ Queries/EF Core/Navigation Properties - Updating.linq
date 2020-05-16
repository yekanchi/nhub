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

// Retrieve a customer:
Customer cust = Customers.Single (c => c.ID == 1);

// Create two purchases:
Purchase p1 = new Purchase { ID = 100, Description="Bike",  Price=500, Date = DateTime.Now };
Purchase p2 = new Purchase { ID = 101, Description="Tools", Price=100, Date = DateTime.Now };

// and add them to the customer's Purchases colleciton.
cust.Purchases.Add (p1);
cust.Purchases.Add (p2);

SaveChanges();

Purchases.RemoveRange (p1, p2);
SaveChanges();

