<Query Kind="Statements">
  <Reference Relative="Chapter11.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter11\Chapter11.exe</Reference>
  <Namespace>Chapter11.Model</Namespace>
  <Namespace>Chapter11.Queries</Namespace>
</Query>

var query = from defect in SampleData.AllDefects
            where defect.AssignedTo != null
            group defect by defect.AssignedTo into grouped
            select new { Assignee=grouped.Key, 
                         Count=grouped.Count() } into result
            orderby result.Count descending
            select result;

foreach (var entry in query)
{
    Console.WriteLine("{0}: {1}",
                      entry.Assignee.Name,
                      entry.Count);
}
