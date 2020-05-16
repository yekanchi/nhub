<Query Kind="Program">
  
</Query>

public static class NumberEx
{
	public static IEnumerable<int> Digits(this int n)
	{
		List<char> chars = new List<char>() {'0','1','2','3','4','5','6','7','8','9'};
		List<int> digits = new List<int>();
		foreach (char c in n.ToString())
			digits.Add(chars.IndexOf(c));
		return digits.AsEnumerable();
	}
}
void Main()
{
	Enumerable.Range(0,1000).Where(k => k.Digits().Select (x => x * x * x).Sum() == k)
   	       .Dump("Armstrong Numbers");
	Enumerable.Range(0,1000).Where(k => 
{
	var digits = k.Digits();
	if(digits.Sum() * digits.Aggregate ((x,y) =>x*y) == k)
		return true;
	else
		return false;
}).Dump("Sum Product Numbers");

Enumerable.Range(0,1000)
		  .Where (e => Math.Pow(e.Digits().Sum(),3) == e)
          .Dump(" Dudeney Numbers");
		  
Enumerable.Range(1,1000)
		  .Where (e => e.Digits()
          .Where (d => d > 0)
          .Select(x =>Enumerable.Range(1,x)
          .Aggregate((a,b) => a*b)) //Calculating factorial of each digit
          .Sum() //Calculating summation of factorials
           == e) //when summation matches number it's a factorion
		  .Dump("Factorions");
}
