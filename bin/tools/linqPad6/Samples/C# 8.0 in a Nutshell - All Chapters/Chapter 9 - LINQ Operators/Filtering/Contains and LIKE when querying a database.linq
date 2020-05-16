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

Customers.Where (c => c.Name.Contains ("a"))  
	.Dump ("Notice the SQL translation uses LIKE");

Customers.Where (c => c.Name.StartsWith ("J"))  
	.Dump ("StartsWith and EndsWith also translate to LIKE");

Customers.Where (c => EF.Functions.Like (c.Name, "_ar%y"))
	.Dump ("A more complex use of LIKE");