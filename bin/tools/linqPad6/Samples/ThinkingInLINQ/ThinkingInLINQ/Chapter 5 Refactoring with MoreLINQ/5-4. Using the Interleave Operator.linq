<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

int[] n1 = {1,3,4,5};
int[] n2 = { 4,6};
int[] total = new int[n1.Length + n2.Length];
int first = 0;
int second = 0;
int index = 0;
for(;index< n1.Length+n2.Length;index++)
{
	if(index % 2 == 0 )
	{
		//Pick element from the first
		if(first!=n1.Length)
		{
			total[index] = n1[first];
			first++;
		}
		else
		{
			if (second != n2.Length)
			{
				total[index] = n2[second];
				second++;
			}
		}
	}
	else
	{
		//Pick element from the second
		if(second!=n2.Length)
		{
			total[index] = n2[second];
			second++;
		}
		else
		{
			if (first != n1.Length)
			{
				total[index] = n1[first];
				first++;
			}
		}
	}
}
total.Dump("Interleaved by Loop");
n1.Interleave(n2).Dump("Interleaved by MoreLINQ");
