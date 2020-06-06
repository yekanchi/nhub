<Query Kind="Statements">
  
</Query>

string sentence = @"This is an effort by the World Health Organization (WHO) and
					Fédération Internationale de Football Association (FIFA) to help
					footballers in poor nations. Associated Press (AP) reports.";
//Add all stop words in this list
List<string> stopWords = new List<string>() {"of", "de"};
foreach(string sw in stopWords)
{
	List<string> matches = Regex
								.Matches(sentence,sw + " " + "[A-Z][a-z]+")
								.Cast<Match>()
								.Select(m => m.Value).ToList();
	foreach (string m in matches)
	{
		sentence = sentence.Replace(m,m.Replace(sw+" ",string.Empty)+"_"+sw);
	}
}
List<string> all = sentence.Split(new char[]{' ',',','!',';','[',']','(',')','-','\'','"','\r','\n'},StringSplitOptions.RemoveEmptyEntries)
                            .ToList();
List<string> abbs = all
						.Where (s => s.ToCharArray().All (x => x>='A' && x<'Z'))
						.Distinct()
						.ToList();

abbs
		.Select (a => new KeyValuePair<int,int>(all.IndexOf(a),a.Length))
		.Select(a => new { Abbreviation = all[a.Key], Expansion = Enumerable.Range(a.Key-a.Value,a.Value)
        .Select (e => all[e])
        .Aggregate((f,g) => f.Split('_').Aggregate ((x,y) => y + " " + x ) + " " + g.Split('_').Aggregate ((x,y) => y + " " + x )).Trim()})
        .OrderBy (a => a.Abbreviation).Dump("Abbreviations with Expansions");
