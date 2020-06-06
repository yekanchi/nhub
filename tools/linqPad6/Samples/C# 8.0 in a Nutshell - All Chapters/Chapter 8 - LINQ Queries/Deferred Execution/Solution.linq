<Query Kind="Statements" />

// We can make the preceding query work correctly by assigning the loop variable to another
// variable declared inside the statement block:

IEnumerable<char> query = "Not what you might expect";
string vowels = "aeiou";

for (int i = 0; i < vowels.Length; i++)
{
	char vowel = vowels[i];
	query = query.Where (c => c != vowel);
}

foreach (char c in query) Console.Write (c);