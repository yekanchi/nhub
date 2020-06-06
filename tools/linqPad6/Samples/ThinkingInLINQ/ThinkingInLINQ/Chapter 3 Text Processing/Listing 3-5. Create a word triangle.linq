<Query Kind="Statements">
  
</Query>

//Word Triangle
string word = "umbrella";
Enumerable
	.Range(1, word.Length)
	.Select (k => new string(word.ToCharArray().Take(k).ToArray()))
	.Concat
		(
				Enumerable.Range(1, word.Length)
				.Select(k => new string(word.ToCharArray()
				.Take(word.Length - k)
				.ToArray()))
		)
	.Aggregate ((m,n) => m + Environment.NewLine + n)
	.Dump("Word Triangle");
