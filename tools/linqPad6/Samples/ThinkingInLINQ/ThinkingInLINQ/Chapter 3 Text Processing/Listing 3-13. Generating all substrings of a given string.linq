<Query Kind="Program">
  
</Query>

public static List<string> NGrams(string sentence, int q)
{
	int total = sentence.Length - q;
	List<string> tokens = new List<string>();
	for (int i = 0; i <= total; i++)
	tokens.Add(sentence.Substring(i, q));
	return tokens;
}
void Main()
{
	string name = "LINQ";
	Enumerable.Range(0,name.Length+1)
	.SelectMany(z => NGrams(name,z))
	.Distinct()
	.Where (b => b.Length!=0)
	.Dump("All substrings of 'LINQ'");
}
