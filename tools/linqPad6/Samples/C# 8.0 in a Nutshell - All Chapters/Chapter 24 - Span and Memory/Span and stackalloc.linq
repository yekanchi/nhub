<Query Kind="Program" />

unsafe void Main()
{
	Span<int> numbers = stackalloc int [1000];
	
	for (int i = 0; i < numbers.Length; i++)
		numbers[i] = i;
	
	int total = Sum (numbers).Dump();
}

int Sum (ReadOnlySpan<int> numbers)
{
	int total = 0;
	foreach (int i in numbers) total += numbers [i];
	return total;
}