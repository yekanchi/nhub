<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

int[] values = {1,2,3,4,5,6,7,8,9,10,11};
List<List<int>> windowVals = new List<List<int>>();
for(int i=0;i<values.Length-1;i++)
{
	List<int> inner = new List<int>();
	for(int j=i;j<i+2;j++)
		inner.Add(values[j]);
	windowVals.Add(inner);
}
List<double> movingAvgs = new List<double>();
for(int i=0;i<windowVals.Count;i++)
{
	double avg = 0;
	for(int j = 0;j<windowVals[i].Count;j++)
		avg += windowVals[i][j];
	movingAvgs.Add(avg/windowVals[i].Count);
}
movingAvgs.Dump("Moving Averages using loops");
values.Windowed(2).Select(list => list.Average()).Dump("Moving Averages using MoreLINQ");