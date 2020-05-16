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

dbContext.Add (new Customer() { Name = "Dylan" });
ShowChanges (dbContext, "Added to context");
// Added to context
// EfCoreLib.Customer is Added
//     ID: '-2147482643' modified: False
//     Name: 'Dylan' modified: False

dbContext.SaveChanges();
ShowChanges (dbContext, "Added customer was saved.");
// Added customer was saved.
// EfCoreLib.Customer is Unchanged
//     ID: '10' modified: False
//     Name: 'Dylan' modified: False

var dylan = dbContext.Customers.First (c => c.Name == "Dylan");

ShowChanges (dbContext, "Customer loaded");
// Customer loaded
// EfCoreLib.Customer is Unchanged
//     ID: '10' modified: False
//     Name: 'Dylan' modified: False

dylan.Name = "Dylan Modified";
ShowChanges (dbContext, "Modified Name property");
// Modified Name property
// EfCoreLib.Customer is Modified
//     ID: '10' modified: False
//     Name: 'Dylan Modified' modified: True

dbContext.SaveChanges();
dbContext.Customers.Remove (dylan);
ShowChanges (dbContext, "Removed from context");
// Removed from context
// EfCoreLib.Customer is Deleted
//     ID: '10' modified: False
//     Name: 'Dylan Modified' modified: False

dbContext.SaveChanges();
ShowChanges (dbContext, "Saved to DB");
// Saved to DB
// (No changes to show)


void ShowChanges (DbContext dbContext, string title)
{
	Console.WriteLine (title);
	foreach (var e in dbContext.ChangeTracker.Entries())
	{
		Console.WriteLine ($"{e.Entity.GetType().FullName} is {e.State}");
		foreach (var m in e.Members)
			Console.WriteLine (
				$"  {m.Metadata.Name}: '{m.CurrentValue}' modified: {m.IsModified}");
	}
}