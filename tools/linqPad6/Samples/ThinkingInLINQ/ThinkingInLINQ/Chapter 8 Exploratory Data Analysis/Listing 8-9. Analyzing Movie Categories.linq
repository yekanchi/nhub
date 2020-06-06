<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

string[] allStrings = File.ReadAllText(@"C:\movies.txt")
.Split(new string[]{"|","\r","\n"},StringSplitOptions.None);
var movies =
Enumerable.Range(0,allStrings.Length/24)
		.ToList()
		.Select ( s => allStrings.Skip(s*24).Take(24))
		.Select (s =>
		{
				return new
				{
					ID = s.ElementAt(0),
					Title = s.ElementAt(1),
					ReleaseDate = s.ElementAt(2).Trim(),
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
					IsWestern = s.ElementAt(22)=="1"?true: false
				};
		});

Dictionary<string,int> moviesPerCategory = new Dictionary<string,int>();
foreach (var movie in movies)
{
	movie
		.GetType()
		.GetProperties()
		.Select (m => new KeyValuePair<string,object>(m.Name, m.GetValue(movie)))
		.Skip(4)//Skipping ID,Title,ReleaseDate and IMDBURL field
		.Where (f => Convert.ToBoolean(f.Value)==true)
		.Select (f => f.Key.Substring(2))
		.ToList()
		.ForEach( k =>
		{
			if(!moviesPerCategory.ContainsKey(k))
				moviesPerCategory.Add(k,1);
			else
				moviesPerCategory[k]++;
		});
}
int totalMovieCount = moviesPerCategory.Select( t => t.Value).Sum();
moviesPerCategory
              .Select (pc => new { Category = pc.Key, Count = pc.Value, Percentage = (100*Math.Round((double)pc.Value/(double)totalMovieCount,2))})
              .OrderByDescending (movie => movie.Percentage)
              .Dump("Movie Categories");

