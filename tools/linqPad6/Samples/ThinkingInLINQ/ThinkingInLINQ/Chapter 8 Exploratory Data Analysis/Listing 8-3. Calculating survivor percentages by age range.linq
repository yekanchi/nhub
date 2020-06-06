<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Func<double,string> AgeGroup =
				x => x!=-1 && x<2?"Infants"
				:x>=2 && x<6 ? "Toddlers"
				:x >= 6 && x<13 ?"Kids"
				:x>=13 && x<=19?"Teenagers"
				:x>=20 && x<30? "Young Adults"
				:x>=30 && x<=35? "Early thirties"
				:x>=36 && x<40? "Late thirties"
				:x>=40 && x<=50? "Middle Aged"
				:x>=51 && x<60 ? "Old"
				:"Retired";
				
				

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
.Select( x =>
		new
		{
			Sex = x.Sex,
			Age = x.Age,
			Embarked = x.Embarked,
			AgeGroup = AgeGroup(x.Age),
			Survived = x.Survived
		}
	   )
.ToLookup (x => x.AgeGroup)
.ToDictionary (x => x.Key, x => new KeyValuePair<double,double>(100*((double)x.Count (z => z.Sex == "female" && z.Survived == "Yes")/(double)x.Count()),
                                                             100*((double)x.Count (z => z.Sex == "male" && z.Survived == "Yes")/(double)x.Count())))
.Select
(
	x => new
	{
		AgeGroup = x.Key,
		FemaleSurvival = Math.Round(x.Value.Key,2),
		MaleSurvival = Math.Round(x.Value.Value,2)
	}
)
.OrderByDescending( x=> x.FemaleSurvival)
.Dump("Agewise survival percentages");
