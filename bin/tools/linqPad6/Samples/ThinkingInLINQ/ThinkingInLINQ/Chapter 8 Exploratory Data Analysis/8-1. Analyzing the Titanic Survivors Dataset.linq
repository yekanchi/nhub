<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

string text = File.ReadAllText(@"C:\titanic.csv");
Regex.Matches(text,"\"[A-Za-z ., ()'-/]+\"").Cast<Match>()
	.Select (m => m.Value)
	.ToList()
	.ForEach( z => text = text.Replace(z, z.Replace(",","[__COMMA__]")));
//A single LINQ query to find out survival percentage of male and female in each class. 
//Kind of long. But you can put Dump method calls anywhere to see what's happening. 
//Like I have done in couple of places before finally dumping the result. 
text.Split(new char[]{'\r','\n'},StringSplitOptions.RemoveEmptyEntries)
	.Skip(1)//Skip the column header row of the CSV file
	.Select (t => t.Split(','))
	.Select (t => new
	{
		PassengerId = t[0],
		Survived = t[1]=="1"?"Yes":"No",
		Pclass = t[2],
		Name = t[3].Replace("[__COMMA__]",","),
		Sex = t[4],
		Age = t[5].Length !=0 ? Convert.ToDouble(t[5]):-1,
		SibSp = t[6],
		Parch = t[7],
		Ticket = t[8],
		Fare = Convert.ToDouble(t[9]),
		Cabin = t[10],
		Embarked = t[11]
	})//At this point the CSV is loaded as a collection of an anonymous type
	.Dump()
	.Select (f => new Tuple<string,double,string,string,double>(f.Pclass,Math.Round(f.Fare,2),f.Survived,f.Sex,f.Age))
	.Dump()
	.ToLookup (f => f.Item1)
	.OrderByDescending (f => f.Key)
	.ToDictionary(f => f.Key, f=> new KeyValuePair<double,double>(100*((double)f.Count (x => x.Item4 == "female" && x.Item3 == "Yes")/
																								(double)f.Count(j => j.Item4=="female")),
																	100*((double)f.Count (x => x.Item4 == "male" && x.Item3 == "Yes")/
																								(double)f.Count(j => j.Item4=="male"))))
	.Select (f => new { PClass = f.Key,FemaleSurvivalRate = Math.Round(f.Value.Key,3),MaleSurvivalRate = Math.Round(f.Value.Value,3)} )
	.OrderByDescending (f => f.FemaleSurvivalRate )
	.Dump("Survivor Percentage per class");

