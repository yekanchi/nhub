<Query Kind="Statements">
  
</Query>

string[] words1 = {"orange", "herbal", "rubble", "indicative", "mandatory","brush", "golden", "diplomatic", "pace"};
string[] words2 = {"verbal", "rush", "pragmatic", "story", "race","bubble", "olden"};
words1
	.Concat(words2)
	.ToLookup(w => w.Substring(w.Length-3))
	.Where(w => w.Count() >= 2)
	.Select(w => w.Aggregate((m,n)=>m+", "+n))
	.Dump("Showing rhyming pairs comma separated");
