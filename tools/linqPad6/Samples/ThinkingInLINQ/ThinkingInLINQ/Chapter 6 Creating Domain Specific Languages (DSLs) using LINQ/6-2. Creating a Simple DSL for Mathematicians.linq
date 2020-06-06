<Query Kind="Program">
  
  <Namespace>MoreLinq</Namespace>
</Query>


	public static class IntEx
	{
		public static int Cube(this int number)
        {
            return number * number * number;
        }
		public static IEnumerable<int> Cube(this IEnumerable<int> digits)
		{
			return digits.Select (d =>d.Cube() );
		}
        public static int Square(this int number)
        {
            return number * number;
        }

        public static IEnumerable<int> Digits(this int number)
        {
            return number.ToString().ToCharArray()
            .Select(n => Convert.ToInt32(n.ToString()));
        }
        public static IEnumerable<int> ReverseDigits(this int number)
        {
            return number.Digits().Reverse();
        }
        public static IEnumerable<int> EvenDigits(this int number)
        {
            return number.ToString().ToCharArray()
            .Where((m, i) => i % 2 == 0).Select(n => Convert.ToInt32(n.ToString()));
        }
        public static IEnumerable<int> OddDigits(this int number)
        {
            return number.ToString().ToCharArray()
            .Where((m, i) => i % 2 != 0).Select(n => Convert.ToInt32(n.ToString()));
        }
        public static bool Are(this IEnumerable<int> actualDigits, params int[] digits)
        {
            return actualDigits.SequenceEqual(digits);
        }
        public static IEnumerable<int> DigitsAt(this int number, params int[] indices)
        {
            var asString = number.ToString();
            return indices.Select(i => Convert.ToInt32(asString[i].ToString()));
        }
        public static bool AreZero(this IEnumerable<int> digits)
        {
            return digits.All(d => d == 0);
        }
        public static int FormNumber(this IEnumerable<int> digits)
        {
            return digits.Select((d, i) => d * (int)Math.Pow(10, digits.Count() - (i + 1)))
            .Aggregate((a, b) => a + b);
        }
        public static IEnumerable<int> Factorial(this IEnumerable<int> digits)
        {
            foreach (var d in digits)
                if (d == 0)
                    yield return 1;
                else
                    yield return Enumerable.Range(1, d).Aggregate((a, b) => a * b);
        }
        public static int Product(this IEnumerable<int> digits)
        {
            return digits.Aggregate((f, s) => f * s);
        }
	}
// Define other methods and classes here
void Main()
{
	Enumerable.Range(1,10000)
			  .Where ( n => n.Digits().Cube().Sum() == n )
              .Dump("Armstrong Numbers");
			  
	Enumerable.Range(0,10000)
			  .Where (d => d. Digits().Sum().Cube() == d)
              .Dump("Dudeney numbers");

    Enumerable.Range(0,10000)
               .Where (d => d. Digits().Factorial().Sum() == d)
               .Dump("Factorions");

    Enumerable.Range(0,10000)
              .Where (d => d. Digits().Sum() * d.Digits().Product() == d)
              .Dump("Sum Product numbers");

}
