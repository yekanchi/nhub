<Query Kind="Statements">
  
</Query>

//finding matching pairs
string[] words1 = {"orange", "herbal", "rubble", "indicative", "mandatory","brush", "golden", "diplomatic", "pace"};
string[] words2 = {"verbal", "rush", "pragmatic", "story", "race","bubble", "olden"};
//Checking whether the last three characters match.
//is a rudimentary way to tell if two words rhyme.
Func<string,string,bool> mightRhyme = (a,b) => a[a.Length-1]==b[b.Length - 1]
                                            && a[a.Length-2]==b[b.Length - 2]
                                            && a[a.Length-3]==b[b.Length - 3];


words1
	.Select(w => new KeyValuePair<string,string>(w, words2.FirstOrDefault(wo => mightRhyme(w,wo))))
	.Where(w => !String.IsNullOrEmpty(w.Value))
	.Dump("Matching Pairs");
