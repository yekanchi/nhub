<Query Kind="Statements" />

// Suppose we want to build up a query that strips all the vowels from a string.
// The following (although inefficient) gives the correct result:

IEnumerable<char> query = "Not what you might expect";

query = query.Where (c => c != 'a');
query = query.Where (c => c != 'e');
query = query.Where (c => c != 'i');
query = query.Where (c => c != 'o');
query = query.Where (c => c != 'u');

new string (query.ToArray()).Dump ("All vowels are stripped, as you'd expect.");

"Now, let's refactor this. First, with a for-loop:".Dump();

string vowels = "aeiou";

for (int i = 0; i < vowels.Length; i++)
	query = query.Where (c => c != vowels[i]);   // IndexOutOfRangeException

foreach (char c in query) Console.Write (c);

// An IndexOutOfRangeException is thrown! This is because, as we saw in Chapter 4 
// (see "Capturing Outer Variables"), the compiler scopes the iteration variable 
// in the for loop as if it was declared outside the loop. Hence each closure captures
// the same variable (i) whose value is 5 when the query is enumerated.