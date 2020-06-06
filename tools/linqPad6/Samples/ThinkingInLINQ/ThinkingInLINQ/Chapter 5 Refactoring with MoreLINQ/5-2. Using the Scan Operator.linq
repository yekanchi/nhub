<Query Kind="Statements">
<NuGetReference>morelinq</NuGetReference>
<Namespace>MoreLinq</Namespace>
</Query>

//For running these queries you shall need MoreLINQ
//http://code.google.com/p/morelinq/source/browse
int[] numbers = {1,2,3,4};
int[] sums = new int[numbers.Length];
for(int i=0;i<numbers.Length;i++)
{
	for(int j = 0;j<=i;j++)
		sums[i]+=numbers[j];
}
sums.Dump();

int[] numbers2 = {1,2,3,4};
numbers2.Scan((a,b)=>a + b).Dump();