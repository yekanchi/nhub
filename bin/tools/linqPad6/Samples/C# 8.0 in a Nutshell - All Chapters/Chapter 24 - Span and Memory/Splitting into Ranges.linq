<Query Kind="Program" />

void Main()
{
	ReadOnlySpan<char> source = "The quick brown fox";
	foreach (var range in Split (source))
	{
		ReadOnlySpan<char> wordSpan = source [range];
		wordSpan.Dump();
	}
}

Range[] Split (ReadOnlySpan<char> input)
{
	int pos = 0;
	var list = new List<Range>();
	for (int i = 0; i <= input.Length; i++)
		if (i == input.Length || char.IsWhiteSpace (input [i]))
		{
			list.Add (new Range (pos, i));
			pos = i + 1;
		}
	return list.ToArray();
}