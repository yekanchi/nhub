<Query Kind="Program" />

// Iterators are highly composable:

static void Main()
{
	foreach (int fib in EvenNumbersOnly (Fibs(6)))
		Console.WriteLine (fib);
}

static IEnumerable<int> Fibs (int fibCount)
{
	for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
	{
		yield return prevFib;
		int newFib = prevFib+curFib;
		prevFib = curFib;
		curFib = newFib;
	}
}

static IEnumerable<int> EvenNumbersOnly (IEnumerable<int> sequence)
{
	foreach (int x in sequence)
		if ((x % 2) == 0)
			yield return x;
}

// See Chapter 7 for more information on Iterators