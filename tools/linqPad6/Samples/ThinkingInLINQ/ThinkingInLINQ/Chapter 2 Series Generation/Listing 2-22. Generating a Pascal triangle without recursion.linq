<Query Kind="Statements">
  
</Query>

List<Tuple<int,int,int>> pascalValues = new List<Tuple<int,int,int>>();
pascalValues.Add(new Tuple<int,int,int>(1,1,1));
pascalValues.Add(new Tuple<int,int,int>(2,1,1));
pascalValues.Add(new Tuple<int,int,int>(2,2,1));
for(int i=1;i<10;i++)
{
	int currentRow = pascalValues.Last().Item1 + 1;
	int currentCol = pascalValues.Last().Item2 + 1;
	for(int j = 1;j<=currentCol;j++)
	{
		if(j==1 || j== currentCol)
			pascalValues.Add(new Tuple<int,int,int>(currentRow,j,1));
		else
			pascalValues.Add(new Tuple<int,int,int>(currentRow,j,
			                 pascalValues.First (v => v.Item1 == currentRow - 1 
					&& v.Item2 == j - 1).Item3 + pascalValues.First (v => v.Item1 == currentRow - 1 
					&& v.Item2 == j).Item3 ));
	}
}
//Show the table
pascalValues
	.ToLookup(t=>t.Item1,t=>t.Item3.ToString())
	.Select (t => t.Aggregate ((x,y) => x + " " + y ))
	.Aggregate ((u,v) => u + Environment.NewLine + v)
	.Dump("Pascal's Triangle");
