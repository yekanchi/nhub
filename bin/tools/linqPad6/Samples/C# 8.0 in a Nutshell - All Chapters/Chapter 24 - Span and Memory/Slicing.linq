<Query Kind="Program" />

void Main()
{
	var numbers = new int [1000];
	for (int i = 0; i < numbers.Length; i++) numbers [i] = i;

	Sum (numbers).Dump ("total - using implicit conversion");
	Sum (numbers.AsSpan()).Dump ("total - using .AsSpan()");
	Sum (numbers.AsSpan (250, 500)).Dump ("total - slicing");

	Span<int> span = numbers;
	Console.WriteLine (span [^1]);            // Last element
	Console.WriteLine (Sum (span [..10]));    // First 10 elements
	Console.WriteLine (Sum (span [100..]));   // 100th element to end
	Console.WriteLine (Sum (span [^5..]));    // Last 5 elements
}

int Sum (ReadOnlySpan<int> numbers)
{
	int total = 0;
	foreach (int i in numbers) total += i;
	return total;
}