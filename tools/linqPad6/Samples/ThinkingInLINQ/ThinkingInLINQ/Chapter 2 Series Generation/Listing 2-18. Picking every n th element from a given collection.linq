<Query Kind="Statements">
  
</Query>

int n = 20; //Pick every 20th element.
List<int> numbers = Enumerable.Range(1,100).ToList();
List<int> nthElements = new List<int>();
Enumerable.Range(0,numbers.Count()/n)
		  .ToList()
          .ForEach(k => nthElements.Add(numbers.Skip(k*n).First()));
nthElements.Dump();
