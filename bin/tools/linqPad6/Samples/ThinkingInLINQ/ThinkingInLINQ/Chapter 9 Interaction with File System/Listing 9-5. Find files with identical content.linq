<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

//Locating exact-duplicate files
Directory.GetFiles(@"C:\Program Files","*.*",SearchOption.AllDirectories)
			.Where (d => d.EndsWith(".txt"))
			.Select (d => new { FileName = d,ContentHash = File.ReadAllText(d).GetHashCode()})
			.ToLookup (d => d.ContentHash)
			.Where (d => d.Count ()>=2)
			.Dump();
