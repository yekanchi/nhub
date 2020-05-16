<Query Kind="Statements" />

int[][] numbers = 
{
	new int[] { 1, 2, 3 },
	new int[] { 4, 5, 6 },
	null                     // this necessitates the null coalescing operator below
};

IEnumerable<int> flat = numbers
	.SelectMany (innerArray => innerArray ?? Enumerable.Empty <int>() );

flat.Dump();