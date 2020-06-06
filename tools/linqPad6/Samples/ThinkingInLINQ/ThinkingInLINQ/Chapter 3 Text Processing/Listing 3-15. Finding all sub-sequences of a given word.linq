<Query Kind="Statements">
  
</Query>

//Locate and change the path of the T9.txt if you have saved it elsewhere.
StreamReader sr = new StreamReader ("C:\\T9.txt");
var allWords = Regex.Matches(sr.ReadToEnd(),"[a-z]+").Cast<Match>().Select (m => m.Value).ToList();
sr.Close();
List<string> subsequences = new List<string>();
string bigWord = "awesome";
foreach (string smallWord in allWords)
{
	var q = smallWord
					.ToCharArray()
					.Select (x => bigWord
					.ToCharArray()
					.ToList()
					.LastIndexOf(x));
	if(q.All (x => x !=-1)
			&&
					q.Take(q.Count () - 1)
					.Select ((x,i) => new {CurrentIndex = x, NextIndex = q.ElementAt(i+1)})
					.All (x => x.NextIndex - x.CurrentIndex > 0))
			{
					subsequences.Add(smallWord);
			}
}
subsequences.Dump("All subsequences");
