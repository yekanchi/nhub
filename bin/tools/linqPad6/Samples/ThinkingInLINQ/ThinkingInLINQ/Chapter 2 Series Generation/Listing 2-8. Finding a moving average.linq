<Query Kind="Statements">
  
</Query>

List<double> numbers = new List<double>(){1,2,3,4};
List<double> movingAvgs = new List<double>();
//moving window is of length 2.
int windowSize = 2;
Enumerable.Range(0,numbers.Count - windowSize + 1)
		  .ToList()
          .ForEach(k => movingAvgs.Add(numbers.Skip(k).Take(windowSize).Average()));
//Listing moving averages
movingAvgs.Dump();
