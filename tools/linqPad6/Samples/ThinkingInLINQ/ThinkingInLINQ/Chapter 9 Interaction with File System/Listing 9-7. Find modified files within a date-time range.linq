<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Directory.GetFiles(@"C:\Program Files","*.*",SearchOption.AllDirectories)
		.Select (d => new FileInfo(d))
		.OrderByDescending (d => d.LastWriteTime)
		.Select (d => new {Name = d.FullName ,LastModifiedTime = d.LastWriteTime})
		.Where (d => d.LastModifiedTime.AddDays(7).CompareTo(DateTime.Today)>=0 )
		.Dump("Files modified during last week");
