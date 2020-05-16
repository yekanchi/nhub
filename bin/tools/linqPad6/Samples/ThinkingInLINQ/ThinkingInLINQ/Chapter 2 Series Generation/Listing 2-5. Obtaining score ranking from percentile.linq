<Query Kind="Statements">
 
</Query>

int[] marks = {20,15,31,34,35,50,40,90,99,100};
marks
.ToLookup(k=>k, k=> marks.Where (n => n>=k))
.Select (k => new 
			{
				Marks = k.Key,
				Rank = 10*((double)k.First().Count()/(double)marks.Length)
			}
		)
.Dump("Ranks");