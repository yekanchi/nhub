<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

unsafe void Main()
{
	var source = "The quick brown fox".AsSpan();
	var ptr = Marshal.AllocHGlobal (source.Length * sizeof (char));
	try
	{
		var unmanaged = new Span<char> ((char*)ptr, source.Length);
		source.CopyTo (unmanaged);
		foreach (var word in unmanaged.Split())
			Console.WriteLine (word.ToString());
	}
	finally { Marshal.FreeHGlobal (ptr); }
}

public readonly ref struct CharSpanSplitter
{
	readonly ReadOnlySpan<char> _input;
	public CharSpanSplitter (ReadOnlySpan<char> input) => _input = input;
	public Rator GetEnumerator() => new Rator (_input);

	public ref struct Rator   // Forward-only enumerator
	{
		readonly ReadOnlySpan<char> _input;
		int _wordPos;
		public ReadOnlySpan<char> Current { get; private set; }

		public Rator (ReadOnlySpan<char> input)
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

	public static CharSpanSplitter Split (this Span<char> input)
		=> new CharSpanSplitter (input);
}