<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Directory.GetFiles(@"C:\Users\mukhsudi\Downloads","*.*",SearchOption.AllDirectories)
			.Select (d => new FileInfo(d))
			.Select (d => new { Directory = d.DirectoryName,FileSize = d.Length} )
            .ToLookup (d => d.Directory )
            .Select (d => new { Directory = d.Key, TotalSizeInMB = Math.Round(d.Select (x => x.FileSize).Sum () / Math.Pow(1024.0,2),2)})
            .OrderByDescending (d => d.TotalSizeInMB)
.Dump();
