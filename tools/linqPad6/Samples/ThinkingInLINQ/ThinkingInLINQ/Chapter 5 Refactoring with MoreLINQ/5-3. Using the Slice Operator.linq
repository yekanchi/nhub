<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

int[] values = {1,2,3,4,5,6,7,8,9,10};
int k = 4;
int start = 3;
int[] slice = new int[k];
for(int i = start,j = 0 ; i< start + k ;i++,j++)
	slice[j] = values[i];
slice.Dump("Sliced from Loop");


values.Slice(3,4).ToArray().Dump("Sliced from LINQ");