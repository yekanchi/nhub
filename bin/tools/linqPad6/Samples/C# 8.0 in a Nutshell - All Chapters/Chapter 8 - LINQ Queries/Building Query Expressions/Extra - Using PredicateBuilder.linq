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
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

// Refer to http://www.albahari.com/expressions/ for info on PredicateBuilder.
//
// Note: PredicateBuilder is built into LINQPad. 
//       To enable, press F4 (for query properties) and go to the 'Advanced' tab.

string[] keywords = { "Widget", "Foo", "Bar" };

var predicate = PredicateBuilder.False<Product>();

foreach (string keyword in keywords)
{
	string temp = keyword;
	predicate = predicate.Or (p => p.Description.Contains (temp));
}

var compiledPredicate = predicate.Compile();

Products.Where (compiledPredicate).Dump ("Notice the multiple OR clauses in the SQL pane");