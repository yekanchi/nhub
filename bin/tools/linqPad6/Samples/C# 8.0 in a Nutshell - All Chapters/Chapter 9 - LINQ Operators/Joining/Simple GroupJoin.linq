<Query Kind="Expression">
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

// NOTE: EF Core currently requires GroupBy without an aggregate expression to be explicitly performed client-side. This is because
//       the query translates into a GroupJoin, which SQL databases don't natively support unless an aggregate expression appears in
//       the SELECT list. The EF Core team takes the position that implicit, rather than explicit, client-side evaluation can mask
//       performance pitfalls. Subsequently, the developer must explicitly force client-side evaluation. This results in two queries
//       to the database, where Linq to SQL generated a single, more efficient query. The EF Core team is tracking the issue and
//       may revisit it at a future date.
//       https://github.com/aspnet/EntityFrameworkCore/issues/17068
from c in Customers.AsEnumerable()
join p in Purchases on c.ID equals p.CustomerID
into custPurchases
select custPurchases