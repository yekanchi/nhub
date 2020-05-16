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

// Note: before delving into this section, make sure you've read the preceding two
// sections: Select and SelectMany. The Join operators are actually unnecessary
// when querying a database, and the equivalent of SQL inner and outer joins is 
// most easily achieved using Select/SelectMany and subqueries!

from c in Customers
join p in Purchases on c.ID equals p.CustomerID
select c.Name + " bought a " + p.Description