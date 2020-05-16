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

var dbContext = this;
Console.WriteLine (dbContext.Customers.Count());

// Insert a new customer
Customer cust = new Customer()
{
	ID = 10,
	Name = "Sara Wells"
};

dbContext.Customers.Add (cust);
dbContext.SaveChanges();    // Writes changes back to database

// Query the database for the customer that was just inserted:
using (var anotherContext = new TypedDataContext (this.Database.GetDbConnection().ConnectionString))
	anotherContext.Customers
		.Single (c => c.Name == "Sara Wells")
		.Dump ("Retrieved from database");

// Update the customer's name, and save the changes to the database:
cust.Name = "Dr. Sara Wells";
dbContext.SaveChanges();

// Delete the customer
Customers.Remove (cust);
dbContext.SaveChanges();

// Click the "SQL" tab to see the SQL commands;