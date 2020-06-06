<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Directory.GetFiles(@"C:\Program Files","*.*",SearchOption.AllDirectories)
.Select (d => new FileInfo(d))
.Where (d => d.Length == 0)
.Dump("Dead Files");
