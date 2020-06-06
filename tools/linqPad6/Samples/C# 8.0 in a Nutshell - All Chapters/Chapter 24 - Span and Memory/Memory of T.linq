<Query Kind="Program" />

void Main()
{
	Split ("Word1 Word2 Word3".AsMemory()).Dump();
}

IEnumerable<ReadOnlyMemory<char>> Split (ReadOnlyMemory<char> input)
{
	int wordStart = 0;
	for (int i = 0; i <= input.Length; i++)
		if (i == input.Length || char.IsWhiteSpace (input.Span [i]))
		{
			yield return input [wordStart..i];   // Slice with C# range operator
			wordStart = i + 1;
		}
}