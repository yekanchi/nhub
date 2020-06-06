<Query Kind="Statements">
  
</Query>

string phrase1 = "the eyes";
string phrase2 = "they see";
phrase1.ToCharArray().Where (p => Char.IsLetterOrDigit(p))
		.OrderBy (p => p)
		.SequenceEqual(phrase2.ToCharArray().Where (p => Char.IsLetterOrDigit(p))
		.OrderBy (p => p))
		.Dump();
