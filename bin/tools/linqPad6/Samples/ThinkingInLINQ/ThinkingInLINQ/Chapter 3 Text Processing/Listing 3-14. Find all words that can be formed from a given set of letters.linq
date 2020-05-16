<Query Kind="Statements">
  
</Query>

Func<string,Dictionary<char,int>> ToHist = word => word.ToCharArray()
			                                           .ToLookup (w => w)
                                                       .ToDictionary(w => w.Key, w => w.Count());
//Scrabble Cheat
string GivenWord = "what";
StreamReader sr = new StreamReader("C:\\T9.txt");
string total = sr.ReadToEnd();
sr.Close();
List<string> allWords = Regex.Matches(total,"[a-z]+")
							 	.Cast<Match>()
							 	.Select (m => m.Value)
								.Distinct()
								.ToList();
								
Dictionary<string,Dictionary<char,int>> forest = new Dictionary<string,Dictionary<char,int>>();
allWords.ForEach(w => forest.Add(w, ToHist(w)));
Dictionary<char,int> hist = ToHist(GivenWord);
List<string> scrabbleCheats = new List<string>();
foreach (string w in forest.Keys)
{
	if(
		//keys should match
		forest[w].Select (x => x.Key).All(x => hist.Select (h => h.Key).Contains(x))
		&&
		//values should be less than or equal to that of histogram
		forest[w].All (x => hist[x.Key]-forest[w][x.Key] >= 0)
	  )
		scrabbleCheats.Add(w);
}
scrabbleCheats.OrderBy (c => c.Length).Dump("Scrabble Cheats");
