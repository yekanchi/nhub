<Query Kind="Program" />

// An operator is overloaded by declaring an operator function:

public struct Note
{
	int value;
	public int SemitonesFromA => value;
	
	public Note (int semitonesFromA) { value = semitonesFromA; }

	public static Note operator + (Note x, int semitones)
	{
		return new Note (x.value + semitones);
	}
	
	// Or more tersely:
	// public static Note operator + (Note x, int semitones) => new Note (x.value + semitones);
	
	// See the last example in "Equality Comparison", Chapter 6 for an example of overloading the == operator
}

static void Main()
{
	Note B = new Note (2);
	Note CSharp = B + 2;	
	CSharp.SemitonesFromA.Dump();
	
	CSharp += 2;	
	CSharp.SemitonesFromA.Dump();
}