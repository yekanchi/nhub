<Query Kind="Statements">
  
</Query>

List<KeyValuePair<int,int>> cumSums = new List<KeyValuePair<int,int>>();
var range = Enumerable.Range(1,10);
range.ToList()
	 .ForEach( k => cumSums.Add(new KeyValuePair<int,int>(k,range.Take(k).Sum())));
cumSums.Dump("Numbers and \"Cumulative Sum\" at each level");
