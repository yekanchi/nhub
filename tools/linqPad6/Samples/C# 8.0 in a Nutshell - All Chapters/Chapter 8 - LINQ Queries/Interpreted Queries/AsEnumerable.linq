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

Regex wordCounter = new Regex (@"\b(\w|[-'])+\b");

// Click the 'SQL' tab below after running this query - notice that only the topic filtering
// predicate executes on SQL Server.

var query = MedicalArticles
	.Where (article => article.Topic == "influenza")
	.AsEnumerable()
	.Where (article => wordCounter.Matches (article.Abstract).Count < 100);

query.Dump();