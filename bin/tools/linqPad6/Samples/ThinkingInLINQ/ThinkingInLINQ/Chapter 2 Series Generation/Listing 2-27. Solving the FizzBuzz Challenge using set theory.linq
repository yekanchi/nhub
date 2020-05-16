<Query Kind="Statements">
  
</Query>

var range = Enumerable.Range(1,40);
var mod3 = range.Where(e => e % 3 == 0);
var mod5 = range.Where(e => e% 5 == 0);
var mod15 = mod3.Intersect(mod5);
//Find numbers that are divisible by 3 but not by 5 or 15
mod3 = mod3.Except(mod15);
//Find numbers that are divisible by 5 but not by 3 or 15
mod5 = mod5.Except(mod15);

//Find integers that are not divisible by either 3 or 5
var neither = range.Except(mod3.Concat(mod5).Concat(mod15));
//Project each of these numbers as per the rule of the challenge.
neither.Select (n => new KeyValuePair<int,string>(n, n.ToString()))
	.Concat(mod3.Select (m => new KeyValuePair<int,string>(m, "fizz")))
	.Concat(mod5.Select (m => new KeyValuePair<int,string>(m, "buzz")))
	.Concat(mod15.Select (m => new KeyValuePair<int,string> (m, "fizzbuzz")))
	//Sort the projected values as per the integer keys
	.OrderBy (n => n.Key)
	//But show the values only.
	.Select (n => n.Value)
	.Take(20) //showing first 20 elements
	//Dump the result
	.Dump ("Fizz Buzz Challenge");
