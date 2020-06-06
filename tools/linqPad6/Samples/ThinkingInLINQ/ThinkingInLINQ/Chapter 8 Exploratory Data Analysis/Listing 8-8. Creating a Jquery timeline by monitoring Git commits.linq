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
//There can be the word "commit" followed by a valid SHA ID of the commit inside a commit
//To bypass these we need to take the minimum length of all these three arrays.
var length = (new List<int>(){commits.Length, authors.Length, dates.Length}).Min();
List<Tuple<string,string,string>> details = new List<Tuple<string,string,string>>();
Enumerable.Range(0,length)
		  .ToList()
		  .ForEach( k => details.Add(new Tuple<string,string,string>(commits[k],authors[k],dates[k])));
var logs = details.Select (d => 
				new
				{
						Author = d.Item2.Substring(d.Item2.IndexOf(':')+1),
						Date = DateTime.ParseExact(d.Item3.Substring(d.Item3.IndexOf(' ')).Trim(),"ddd MMM d HH:mm:ss yyyy zzz",
						CultureInfo.InvariantCulture)
				});
DateTime startDate = logs.OrderBy (l => l.Date).First ().Date.Date;
DateTime endDate = logs.OrderBy (l => l.Date).Last().Date.Date;
startDate.Dump("Start Date");
var logMap = logs
				.ToLookup (l => l.Date.Date)
				.ToDictionary (l => l.Key, l => l.Count());

List<int> commitCounts = new List<int>();
for(;startDate!=endDate;startDate = startDate.AddDays(1))
{
	if(logMap.ContainsKey(startDate))
		commitCounts.Add(logMap[startDate]);
	else
		commitCounts.Add(0);
}
commitCounts.Count.Dump();
StreamWriter sw = new StreamWriter("C:\\dataJQuery.txt");
sw.WriteLine(commitCounts.Select (c => c.ToString()).Aggregate ((m,n) => m + "," + n));
sw.Close();
