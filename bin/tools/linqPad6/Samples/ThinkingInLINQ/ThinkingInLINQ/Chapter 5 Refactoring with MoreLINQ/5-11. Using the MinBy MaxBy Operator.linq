<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

int[] distances = {23,41,11,34,45};
int x = distances[0];
for(int i = 0;i<distances.Length;i++)
	if(distances[i]-10<x-10)
		x = distances[i];
x.Dump();



//The value that minimizes the given function f(x) = x - 10
//in this case
distances.MinBy(a => a - 10).Dump();
//The minimum value of the values projected by the given
//..formula distances.
distances.Min (a => a - 10).Dump();