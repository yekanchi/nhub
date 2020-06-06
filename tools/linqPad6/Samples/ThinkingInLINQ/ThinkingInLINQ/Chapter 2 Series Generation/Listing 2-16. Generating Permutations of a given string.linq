<Query Kind="Program">
  
</Query>

private HashSet<string> GeneratePartialPermutation(string word)
{
		return new HashSet<string>(
		           Enumerable.Range(0,word.Length)
				             .Select(i => word.Remove(i,1).Insert(0,word[i].ToString())));
}
void Main()
{
HashSet<string> perms = GeneratePartialPermutation("abc");
Enumerable.Range(0,2)
.ToList()
.ForEach
(
	c=>
	{
		Enumerable.Range(0,perms.Count ())
				  .ToList()
				  .ForEach
				  (
						i => GeneratePartialPermutation(perms.ElementAt(i)).ToList().ForEach(p=>perms.Add(p))
                  );
	  Enumerable.Range(0,perms.Count ())
				.ToList()
				.ForEach
				(
						i => GeneratePartialPermutation(new string(perms.ElementAt(i).ToCharArray().Reverse().ToArray())
				)
				.ToList().ForEach(p=>perms.Add(p)));
});
perms
		.OrderBy (p => p)
		.Dump("Permutations of 'abc'");
}
