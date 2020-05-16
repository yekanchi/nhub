<Query Kind="Statements">
  <Reference Relative="Chapter11.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter11\Chapter11.exe</Reference>
  <Namespace>Chapter11.Model</Namespace>
  <Namespace>Chapter11.Queries</Namespace>
</Query>

var query = from user in SampleData.AllUsers
            from project in SampleData.AllProjects
select new { User=user, Project=project };

foreach (var pair in query)
{
    Console.WriteLine("{0}/{1}", 
                      pair.User.Name,
                      pair.Project.Name);
}
