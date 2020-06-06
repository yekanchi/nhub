<Query Kind="Program" />

void Main()
{
	ReadOnlySpan<char> source = "The quick brown fox";
	foreach (var word in source.Split())
		word.ToString().Dump();
}

public readonly ref struct CharSpanSplitter
{
	readonly ReadOnlySpan<char> _input;
	public CharSpanSplitter (ReadOnlySpan<char> input) => _input = input;
	public Enumerator GetEnumerator() => new Enumerator (_input);

	public ref struct Enumerator   // Forward-only enumerator
	{
		readonly ReadOnlySpan<char> _input;
		int _wordPos;
		public ReadOnlySpan<char> Current { get; private set; }

		public Enumerator (ReadOnlySpan<char> input)
		{
			_input = input;
			_wordPos = 0;
			Current = default;
		}

		public bool MoveNext()
		{
			for (int i = _wordPos; i <= _input.Length; i++)
				if (i == _input.Length || char.IsWhiteSpace (_input [i]))
				{
					Current = _input [_wordPos..i];
					_wordPos = i + 1;
					return true;
				}
			return false;
		}
	}
}

public static class CharSpanExtensions
{
	public static CharSpanSplitter Split (this ReadOnlySpan<char> input)
		=> new CharSpanSplitter (input);
}