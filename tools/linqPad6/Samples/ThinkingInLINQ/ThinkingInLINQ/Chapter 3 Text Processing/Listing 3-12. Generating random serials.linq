<Query Kind="Statements">
  
</Query>

//Serial generation
for(int i=0;i<5;i++)
{
	Enumerable
			.Range(65,26)
			.Select (e => ((char)e).ToString())
			.Concat(Enumerable.Range(97,26).Select (e => ((char)e).ToString()))
			.Concat(Enumerable.Range(0,10).Select (e => e.ToString()))
			.OrderBy (e => Guid.NewGuid())
			.Take(8)
			.ToList().ForEach (e => Console.Write(e));
			//Give a line break between two random serials
			Console.WriteLine();
}
