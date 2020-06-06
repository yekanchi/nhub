<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

//Locating duplicate files
Directory.GetFiles(@"C:\Users\mukhsudi\Downloads","*.*",SearchOption.AllDirectories)
			.Select (d => new FileInfo(d))
			.Select (d => new {FileName = d.Name,Directory = d.DirectoryName})
			.ToLookup (d => d.FileName)
			.Where (d => d.Count ()>=2)
			.Dump();
