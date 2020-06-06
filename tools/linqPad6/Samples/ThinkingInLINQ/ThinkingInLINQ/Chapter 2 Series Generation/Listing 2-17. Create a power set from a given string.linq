<Query Kind="Program">
  
</Query>

void Main()
{
		string word = "abc";
		HashSet<string> perms = GeneratePartialPermutation(word);
		Enumerable.Range(0,word.Length).ToList().ForEach(x=>
		Enumerable.Range(0,word.Length)
			      .ToList()
                 .ForEach( z=>
				 {
					perms.Add(perms.ElementAt(x).Substring(0,z));
					perms.Add(perms.ElementAt(x).Substring(z+1));
				}));
		
		perms.Select (p => new string(p.ToCharArray()
			 .OrderBy (x => x)
			 .ToArray()))
             .Distinct()
             .OrderBy (p =>p.Length )
             .Dump("Power-set of 'abc'");
}
