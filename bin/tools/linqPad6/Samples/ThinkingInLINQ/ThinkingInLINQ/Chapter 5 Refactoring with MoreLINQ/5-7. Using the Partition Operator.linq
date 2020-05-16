<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

int[] values = {1,2,3,4,5,6,7,8,9,10};
//We want a List<List<int>>
//with values distributed according to the
//given percentages.
//This means there will need to be three lists
//where the first one will have 30% elements
//second one 60% and the last one 10%.
int[] percentages = {30,60,10};
int[] numbersOfItems =
new int[percentages.Length];
for(int i = 0;i<percentages.Length;i++)
	numbersOfItems[i] = (int) Math.Floor((double)values.Length*percentages[i]/100);
List<List<int>> distributions = new
List<List<int>>();
for(int i = 0;i<numbersOfItems.Length;i++)
{
	List<int> innerList = new List<int>();
	if(i==0)
	{
		for(int j=0;j<numbersOfItems[i];j++)
			innerList.Add(values[j]);
	}
	else
	{
		int index = 0;
		for(int k = 0;k<i;k++)
			index+= numbersOfItems[k];
		for (int j = index; j < index +
			numbersOfItems[i];j++ )
		innerList.Add(values[j]);
	}
	distributions.Add(innerList);
}
distributions.Dump("Partitioned as perpercentage using Loop");


int[] numbersOfItemsLINQ = percentages
									.Select (n=> (int) Math.Floor( (double)values.Length*n/100))
									.ToArray();
values.Partition(numbersOfItemsLINQ).Dump("Partitioned as percentage using LINQ");