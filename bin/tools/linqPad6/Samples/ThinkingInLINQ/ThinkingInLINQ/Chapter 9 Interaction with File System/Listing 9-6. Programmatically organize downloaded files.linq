<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

string[] keywords = {"Roslyn","Rx","LINQ","F#"};
string[] videoFormats = {".mp4",".mpg",".mpeg",".flv"};
string[] slides = {".pptx",".ppt"};
string[] articles = {".pdf",".doc",".docx"};
string[] blogs = {".html",".htm"};
Directory.GetFiles(@"C:\Users\mukhsudi\Downloads")
		.ToLookup (d => keywords.FirstOrDefault(x => d.Contains(x)))
		.Where (d => d.Key != null )
		.Select (d =>
		new
		{
			Key = d.Key,
			//Find all the videos for the given keyword
			Videos = d.Where (x => videoFormats.Any (f => x.EndsWith(f))),
			//Find all the articles
			Articles = d.Where (x => articles.Any (f => x.EndsWith(f)))
		    //I omitted Slides and Blogs because those will be similar.
		})
.ToList()
.ForEach(z =>
{
	Directory.CreateDirectory(z.Key + " Videos");
	z.Videos.ToList().ForEach(f =>File.Copy(f,Path.Combine(z.Key + " Videos",new FileInfo(f).Name)));
	Directory.CreateDirectory(z.Key + " Articles");
	z.Articles.ToList().ForEach(f => File.Copy(f,Path.Combine(z.Key + " Articles",new FileInfo(f).Name)));
});
