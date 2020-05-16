<Query Kind="Expression">
  
</Query>

//These are available currencies
int[] curvals = {500,100,50,20,10,5,2,1,1000};
int amount = 2548;
Dictionary<int,int> map = new Dictionary<int,int>();
curvals.OrderByDescending (c => c)
	   .ToList()
       .ForEach(c => {map.Add(c,amount/c); amount = amount % c;});
      
map.Where (m => m.Value!=0)
   .Dump();