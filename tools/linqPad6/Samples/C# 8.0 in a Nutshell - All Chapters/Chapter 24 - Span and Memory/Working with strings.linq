<Query Kind="Program" />

void Main()
{
	CountWhitespace ("Word1 Word2").Dump();
	CountWhitespace ("1 2 3 4 5".AsSpan (3,3)).Dump();

	var span = "This ".AsSpan();                    // ReadOnlySpan<char>
	Console.WriteLine (span.StartsWith ("This"));   // True
	Console.WriteLine (span.Trim().Length);         // 4
}

int CountWhitespace (ReadOnlySpan<char> s)
{
	int count = 0;
	foreach (char c in s)
		if (char.IsWhiteSpace (c))
			count++;
	return count;
}