<Query Kind="Program">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	Func<long,long,long> A015531Rule = (x,y) => 4 *x + 5*y;
	Func<long,long,long> fibonacciRule = (x,y) => x + y;
	Func<double,double> arbitraryRule = (x) => 1/(x + 1/x);
	
	SequenceEx.StartWith<long>(0,1)
			  .ThenFollow(A015531Rule)
			  .Take(5)
              .Dump("A015531");

	SequenceEx.StartWith<long>(1,1)
		  .ThenFollow(fibonacciRule)
		  .Take(5)
          .Dump("First few Fibonacci Numbers");

	SequenceEx.StartWith(1.0)
          .ThenFollow(arbitraryRule)
          .Take(5)
          .Dump("Arbitrary Sequence");
}
	public static class SequenceEx
    {
        public static IEnumerable<T> StartWith<T>(params T[] seeds)
        {
            return new List<T>(seeds).AsEnumerable();
        }
        public static IEnumerable<T> ThenFollow<T>(this IEnumerable<T> thisSequence,
        Func<T, T, T> rule) where T : IEquatable<T>
        {
            while (true)
            {
                T last = thisSequence.ElementAt(thisSequence.Count() - 1);
                T lastButOne = thisSequence.ElementAt(thisSequence.Count() - 2);
                thisSequence = thisSequence
                .Concat((new List<T>() { rule.Invoke(last, lastButOne) }).AsEnumerable());
                yield return rule.Invoke(last, lastButOne);
            }
        }
        public static IEnumerable<T> ThenFollow<T>(this IEnumerable<T> thisSequence, Func<T, T> rule)
        where T : IEquatable<T>
        {
            while (true)
            {
                T last = thisSequence.ElementAt(thisSequence.Count() - 1);
                thisSequence = thisSequence.Concat((new List<T>() { rule.Invoke(last) }).AsEnumerable());
                yield return rule.Invoke(last);
            }
        }
    }

// Define other methods and classes here

