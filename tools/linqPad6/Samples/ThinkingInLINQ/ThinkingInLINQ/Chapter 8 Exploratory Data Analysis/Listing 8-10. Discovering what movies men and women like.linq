<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

string[] allStrings = File.ReadAllText(@"C:\movies.txt")
						  .Split(new string[]{"|","\r","\n"},StringSplitOptions.None);
var movies = Enumerable.Range(0,allStrings.Length/24)
						.ToList()
						.Select ( s => allStrings.Skip(s*24).Take(24))
.Select (s =>
{
		return new
		{
		ID = s.ElementAt(0),
		Title = s.ElementAt(1),
		ReleaseDate = s.ElementAt(2),
		IMDBURL = s.ElementAt(4),
		IsAction = s.ElementAt(5)=="1"?true:false,
		IsAdventure = s.ElementAt(6)=="1"?true:false,
		IsAnimation = s.ElementAt(7)=="1"?true:false,
		IsChildrens = s.ElementAt(8)=="1"?true:false,
		IsComedy = s.ElementAt(9)=="1"?true:false,
		IsCrime = s.ElementAt(10)=="1"?true:false,
		IsDocumentary = s.ElementAt(11)=="1"?true:false,
		IsDrama = s.ElementAt(12)=="1"?true:false,
		IsFantasy = s.ElementAt(13)=="1"?true:false,
		IsFilm_Noir = s.ElementAt(14)=="1"?true:false,
		IsHorror = s.ElementAt(15)=="1"?true:false,
		IsMusical = s.ElementAt(16)=="1"?true:false,
		IsMystery = s.ElementAt(17)=="1"?true:false,
		IsRomance = s.ElementAt(18)=="1"?true:false,
		IsSci_Fi = s.ElementAt(19)=="1"?true:false,
		IsThriller = s.ElementAt(20)=="1"?true:false,
		IsWar = s.ElementAt(21)=="1"?true:false,
		IsWestern = s.ElementAt(22)=="1"?true:false
		};
})
.ToLookup (s => s.ID);
//Loading users in a collection
var users = File.ReadAllText(@"C:\users.txt")
				.Split(new char[]{'\r','\n'},StringSplitOptions.RemoveEmptyEntries)
				.Select (f => f.Split('|'))
				.Select (f => new { ID = f[0], Age = f[1], Sex = f[2],Profession = f[3], ZIP = f[4]} )
				.ToLookup(f => f.ID);
//Loading movie ratings
var movieRatingTokens = File.ReadAllText(@"C:\movieRatings.txt")
							.Split(new char[]{' ','\t','\r','\n'},StringSplitOptions.RemoveEmptyEntries);
var movieRatings = Enumerable.Range(0,movieRatingTokens.Length/4)
							 .Select( k => movieRatingTokens.Skip(4*k).Take(4))
                             .Select (k => new { UserID = k.ElementAt(0), MovieID = k.ElementAt(1), Rating = Convert.ToInt32( k.ElementAt(2)), 
							                     TimeStamp = k.ElementAt(3) } )
.Select (k =>
{
		var currentUser = users[k.UserID].First();
		var movie = movies[ k.MovieID].First();
		return new 
		{ 
			Age = currentUser.Age, 
			Sex = currentUser.Sex,
			Movie = movie.Title, Rating = k.Rating ,
			IsAdenture = movie.IsAdventure,
			IsAnimation = movie.IsAnimation,
			IsChildrens = movie.IsChildrens,
			IsComedy = movie.IsComedy,
			IsCrime = movie.IsCrime,
			IsDocumentary = movie.IsDocumentary,
			IsDrama = movie.IsDrama,
			IsFantasy = movie.IsFantasy,
			IsFilm_Noir = movie.IsFilm_Noir,
			IsHorror = movie.IsHorror,
			IsMusical = movie.IsMusical,
			IsMystery = movie.IsMystery,
			IsRomance = movie.IsRomance,
			IsSci_Fi = movie.IsSci_Fi,
			IsThriller = movie.IsThriller,
			IsWar = movie.IsWar,
			IsWestern = movie.IsWestern
		};
} );
Dictionary<string,Dictionary<string,int>> genderBias = new Dictionary<string,Dictionary<string,int>>();
genderBias.Add("M",new Dictionary<string,int>());
genderBias.Add("F",new Dictionary<string,int>());

//Taking the first 100 movies. If you want to do it for all
//it will take a long time. 
foreach (var mr in movieRatings.Take(100))
{
	string strRep = mr.ToString();
	string key = strRep.Contains("Sex = M")?"M":"F";
	var matches = Regex.Matches(strRep,"Is[A-Za-z_ ]+= True")
						.Cast<Match>()
						.Select (m => m.Value)
						.Select (m => m.Substring(2,m.IndexOf('=')-2)
						.Trim());
	foreach (var m in matches)
	{
		if(!genderBias[key].ContainsKey(m))
			genderBias[key].Add(m,1);
		else
			genderBias[key][m]++;
	}
}

var pieData = genderBias.ToDictionary (b => b.Key,
						b => b.Value
							  .Select
							  (
									v =>
									new
									{
										Key = v.Key ,
										Liking = (double)v.Value/(double)(b.Value.Select (va => va.Value).Sum ())
									}
								)
						   	.OrderByDescending (v => v.Liking )
							.ToDictionary (v => v.Key))
							.Select (b => b.Key + "->" + b.Value.Select (v => "['" + v.Key + "'," + 100 * Math.Round( v.Value.Liking,2) +"]")
							.Aggregate ((m,n) => m + "," + n));
Console.WriteLine(pieData);
