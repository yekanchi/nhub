<Query Kind="Statements">
  
</Query>

List<ulong> fibonacciNumbers = new List<ulong>();
Enumerable.Range(0,200)
		  .ToList()
	      .ForEach(k => fibonacciNumbers.Add(k <= 1 ? 1:
                        fibonacciNumbers[k-2] + fibonacciNumbers[k-1]));
                

fibonacciNumbers.Take(10).Dump("Fibonacci Numbers");
