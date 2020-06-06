<Query Kind="Program" />

// You can implement custom indexers with the this keyword:

class Sentence
{
	string[] words = "The quick brown fox".Split();
	
	public string this [int wordNum]      // indexer
	{ 
		get { return words [wordNum];  }
		set { words [wordNum] = value; }
	}

	// In C# 8, we can also define indexers that use the Index & Range types:
	public string this [Index index] => words [index];
	public string[] this [Range range] => words [range];

}

static void Main()
{		
	Sentence s = new Sentence();
	Console.WriteLine (s[3]);       // fox
	s[3] = "kangaroo";
	Console.WriteLine (s[3]);       // kangaroo

	// Test the indexers that use C#'s Indices and Ranges:

	Console.WriteLine (s [^1]);                // fox  
	string[] firstTwoWords = s [..2].Dump();   // (The, quick)
}