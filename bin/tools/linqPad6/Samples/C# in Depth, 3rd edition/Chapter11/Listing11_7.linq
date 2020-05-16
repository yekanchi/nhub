<Query Kind="Statements">
  <Reference Relative="Chapter11.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter11\Chapter11.exe</Reference>
  <Namespace>Chapter11.Model</Namespace>
</Query>

User tim = SampleData.Users.TesterTim;

var query = from bug in SampleData.AllDefects 
            where bug.Status != Status.Closed
            where bug.AssignedTo == tim
            select bug.Summary;

foreach (var summary in query)
{
    Console.WriteLine(summary);
}
