<Query Kind="Statements">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

int[] lengths = {1,2,3,4,5,6,7};
int[] breadths = {1,1,2,3,1,3};
int[] heights = {2,1,3,1,4};
List<int> volumes = new List<int>();
for(int r = 0;r<lengths.Length; r++)
{
	for(int c = 0; c< breadths.Length; c++)
	{
		for(int z=0;z<heights.Length;z++)
		{
			volumes.Add(lengths[r]*breadths[c]*heights[z]);
		}
	}
}
volumes.Dump("Cartesian using Loops");


List<int> volumesLINQ = lengths
							.Cartesian(breadths, (b,l)=> b * l)
							.Cartesian(heights, (a,b)=> a * b)
							.ToList();
volumesLINQ.Dump("Cartesian using MoreLINQ");

volumes.SequenceEqual(volumesLINQ).Dump("Same?");