<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

var babyNames = File.ReadAllLines(@"C:\baby-names.csv")
.Select (f => f.Split(','))
.Skip(1) //Skip the header row
.Select
(
	a =>	
	new
	{
		Year = Convert.ToInt32(a[0]),
		Name = a[1].Trim(new char[]{'"',' '}),
		Percentage = Convert.ToDouble(a[2]),
		Sex = a[3].Trim(new char[]{'"',' '})
	}
);
babyNames
		.Where (n => n.Sex == "boy") // This analysis is being done for baby boy names.
		.ToLookup (n => n.Name)
		.ToDictionary (n => n.Key )
		.Select (n =>
			new {
					Name = n.Key,
					Popularity = n.Value
					.Select (v => new { Year = v.Year,PopularityPercentage = v.Percentage})
					.ToList()
				})
		.OrderByDescending (n => n.Popularity.Select (p => p.PopularityPercentage )
		.Average ())
		.Take(10) //Show top 10 names as per the average popularity
		.Dump("Popularity of top 10 baby \"boy\" names over the years");
