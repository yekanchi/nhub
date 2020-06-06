<Query Kind="Statements">
  <Reference Relative="Chapter11.exe">&lt;MyDocuments&gt;\Computing\Books\C# in Depth\Third edition\Code\LINQPad\Chapter11\Chapter11.exe</Reference>
  <Namespace>Chapter11.Model</Namespace>
  <Namespace>Chapter11.Queries</Namespace>
</Query>

var query = from left in Enumerable.Range(1, 4)
            from right in Enumerable.Range(11, left)
            select new { Left=left, Right=right };

foreach (var pair in query)
{
    Console.WriteLine("Left={0}; Right={1}", 
                      pair.Left, pair.Right); 
}
