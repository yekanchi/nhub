<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Directory.GetFiles(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5","*.dll")
.SelectMany (d => Assembly.LoadFrom(d).GetTypes()
.Where (a => a.IsClass && a.IsPublic)
.Select (a =>new { Namespace = a.Namespace,
Name = a.Name,
Length = a.Name.Length}))
.ToLookup (d => d.Length)
.OrderByDescending (d => d.Key)
.Select (d => d.ElementAt(0) )
.Take(20)
.Dump("Top 20 most verbose types in .NET 3.5");
