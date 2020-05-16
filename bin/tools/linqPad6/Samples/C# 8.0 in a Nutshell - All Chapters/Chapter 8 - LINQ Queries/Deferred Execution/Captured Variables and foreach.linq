<Query Kind="Statements" />

// Let's now see what happens when you capture the iteration variable of a foreach loop:

IEnumerable<char> query = "Not what you might expect";
string vowels = "aeiou";

foreach (char vowel in vowels)
	query = query.Where (c => c != vowel);

foreach (char c in query) Console.Write (c);

// The output depends on which version of C# you're running! In C# 4.0 and C# 3.0, we
// get the same problem we had with the for-loop: each loop iteration captures the same
// variable, whose final value is 'u'. Hence only the 'u' is stripped. The workaround
// for this is to use a temporary variable (see next example).

// From C# 5.0, they fixed the compiler so that the iteration variable of a foreach loop
// is treated as *local* to each loop iteration. Hence our example strips all vowels
// as expected.