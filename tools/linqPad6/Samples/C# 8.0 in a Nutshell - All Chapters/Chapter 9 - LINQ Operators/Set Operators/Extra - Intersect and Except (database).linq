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

IQueryable<string> purchaseDescriptions = Purchases.Select (p => p.Description);
IQueryable<string> itemDescriptions = PurchaseItems.Select (pi => pi.Detail);

purchaseDescriptions.Intersect (itemDescriptions)
	.Dump ("Purchases that have an identical description in PurchaseItem");
	
purchaseDescriptions.Except (itemDescriptions)
	.Dump ("Purchases that have no corresponding description in PurchaseItem");
	
itemDescriptions.Except (purchaseDescriptions)
	.Dump ("PurchaseItems that have no corresponding description in Purchase");