<Query Kind="Statements">
  <Reference Relative="Chapter11.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter11\Chapter11.exe</Reference>
  <Namespace>Chapter11.Model</Namespace>
</Query>

var query = from user in SampleData.AllUsers
            orderby user.Name.Length
            select user.Name;

foreach (var name in query)
{
    Console.WriteLine("{0}: {1}", name.Length, name);
}
