<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

//Find conceptual load for all namespaces in .NET 3.5
//Conceptual load is the total number of public types in the namespace
Directory.GetFiles(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5","*.dll")
.SelectMany (d => Assembly.LoadFrom(d).GetTypes()
.Where (a => a.IsClass && a.IsPublic))
.ToLookup (d => d.Namespace)
.ToDictionary (d => d.Key, d => d.Count ())
.OrderByDescending (d => d.Value )
.Take(10)//Only the first 10 elements are shown
.Dump();
