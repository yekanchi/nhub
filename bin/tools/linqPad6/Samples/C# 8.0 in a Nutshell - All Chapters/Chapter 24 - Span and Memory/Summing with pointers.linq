<Query Kind="Program" />

unsafe void Main()
{
	int* numbers = stackalloc int [1000];   // Allocate array on the stack

	for (int i = 0; i < 1000; i++)
		numbers [i] = i;

	int total = Sum (numbers, 1000).Dump();
}

unsafe int Sum (int* numbers, int length)
{
	int total = 0;
	for (int i = 0; i < length; i++) total += numbers [i];
	return total;
}