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

// The samples that demonstrate querying a database rely on a simple SQL Server demo
// database contained in a file called "nutshell.mdf" which is automatically unpacked.

// You must have SQL Server (preferably localdb) installed to make these work.
//
// You can test it now by pressing F5:

Purchases.Dump ("Hooray - you have SQL Server localdb installed!");

// If you don't have localdb, but you do have SQL Server:
//
//   1. Right-click "Nutshell" connection on the left, and choose "Properties".
//   2. Change the server name to your SQL Server instance name
//   3. Click "Remember this connection" and OK
//
// Alternatively, if you'd prefer not to attach the nutshell.mdf file:
// 
//   1. Right-click "Nutshell.mdf" on the left, and choose "Edit".
//   2. Select an empty database (or create a new one) on a server of your choice.
//   3. Click "Remember this connection" and OK
//   4. Run the "Populate demo database" script in the next example.