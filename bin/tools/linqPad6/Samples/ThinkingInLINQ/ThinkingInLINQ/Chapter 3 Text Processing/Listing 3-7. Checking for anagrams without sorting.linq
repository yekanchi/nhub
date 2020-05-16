<Query Kind="Statements">
  
</Query>

string phrase1 = "the eyes";
string phrase2 = "they see";
var characterHistogram1 = phrase1
		.ToCharArray()
		.Where (p => Char.IsLetterOrDigit(p))
		.ToLookup (p => p)
		.ToDictionary (p => p.Key, p=>p.Count ());
var characterHistogram2 = phrase2
		.ToCharArray()
		.Where (p => Char.IsLetterOrDigit(p))
		.ToLookup (p => p)
		.ToDictionary (p => p.Key, p=>p.Count ());
		
bool isAnagram = characterHistogram1
                .All(d => characterHistogram2[d.Key] == characterHistogram1 [d.Key]);
				
				
