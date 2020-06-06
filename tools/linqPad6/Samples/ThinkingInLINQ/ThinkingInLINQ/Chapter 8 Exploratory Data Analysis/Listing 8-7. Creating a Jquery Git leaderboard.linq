<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

string log = File.ReadAllText("C:\\jquerygitlogs.txt");
string[] commits = Regex.Matches(log,"commit [a-zA-Z0-9]{40}")
						.Cast<Match>()
						.Select (m => m.Value)
						.ToArray();
string[] authors = Regex.Matches(log,"Author: [a-zA-Z0-9-. @<>']+")
						.Cast<Match>()
						.Select (m => m.Value)
						.ToArray();
string[] dates = Regex.Matches(log,"Date: [a-zA-Z0-9-:+ ]+")
						.Cast<Match>()
						.Select (m => m.Value)
						.ToArray();
List<Tuple<string,string,string>> details = new List<Tuple<string,string,string>>();

Enumerable.Range(0,5000)
		  .ToList()
          .ForEach( k => details.Add(new Tuple<string,string,string>(commits[k],authors[k],dates[k])));
 

details
      .Select
	  (
	   d =>
        new 
       {
		   Author = d.Item2.Substring(d.Item2.IndexOf(':')+1),
		   Date = DateTime.ParseExact(d.Item3.Substring(d.Item3.IndexOf(' ')).Trim(),"ddd MMM d HH:mm:ss yyyy zzz",CultureInfo.InvariantCulture),
		   Location = d.Item3.EndsWith("-0700")?"USA/Canada":"Elsewhere"
        }
      )
.ToLookup (d => d.Author)
.Select (d => new { Author = d.Key, CommitCount = d.Count()})
.OrderByDescending (d => d.CommitCount )
.Take(10)
.Dump("JQuery Leaderboard");
