<Query Kind="Statements">
  <Reference Relative="Chapter11.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter11\Chapter11.exe</Reference>
  <Namespace>Chapter11.Model</Namespace>
</Query>

var query = from user in SampleData.AllUsers
            let length = user.Name.Length
            orderby length
            select new { Name = user.Name, Length = length };

foreach (var entry in query)
{
    Console.WriteLine("{0}: {1}", entry.Length, entry.Name);
}
