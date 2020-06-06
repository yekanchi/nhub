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

Products.RemoveRange (Products.Where (p => p.ID == 999));
Products.Add (new Product { ID = 999, Description = "Test", LastSale = DateTime.Now } );
SaveChanges();

Product[] localProducts = Products.ToArray();

Expression<Func<Product, bool>> isSelling = 
	p => !p.Discontinued && p.LastSale > DateTime.Now.AddDays (-30);

IQueryable<Product> sqlQuery = Products.Where (isSelling);
IEnumerable<Product> localQuery = localProducts.Where (isSelling.Compile());

sqlQuery.Dump ("SQL Query");
localQuery.Dump ("Local Query, using same predicate");