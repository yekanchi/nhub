<Query Kind="Statements">
  
</Query>

List<string> fizzBuzzes = new List<string>();
Enumerable.Range(1,100).ToList().ForEach(k =>
fizzBuzzes.Add(k % 15 == 0 ? "FizzBuzz" : k % 5 == 0 ? "Buzz"
                                        : k % 3 == 0 ? "Fizz"
                                        : k.ToString()));
fizzBuzzes.Take(20).Dump(); //show the first 20 elements
