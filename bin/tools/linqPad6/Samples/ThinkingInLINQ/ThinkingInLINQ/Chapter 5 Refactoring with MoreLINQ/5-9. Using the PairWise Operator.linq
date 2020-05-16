<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	"LLIIIINNQQ".ToCharArray().RemoveConsecutiveDuplicates().Dump("Removed consecutive duplicates from LLIIIINNQQ");
}
public static class MyLinqEx
{
	public static IEnumerable<T> RemoveConsecutiveDuplicates<T>(this IEnumerable<T> input) where T:IComparable
	{
		var conditions = input.Pairwise((a,b)=>a.Equals(b));
		var dontPickIndices = conditions.Index()
										.Where (c => c.Value==true)
										.Select(k => k.Key);
		return Enumerable.Range(0,input.Count())
						 .Where (e => !dontPickIndices.Contains(e))
						 .Select(k => input.ElementAt(k));
	}
}
// Define other methods and classes here
