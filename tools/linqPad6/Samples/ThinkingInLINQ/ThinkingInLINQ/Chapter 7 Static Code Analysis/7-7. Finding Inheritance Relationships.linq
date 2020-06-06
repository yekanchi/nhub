<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Directory.GetFiles(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5","*.dll")
.SelectMany (d => Assembly.LoadFrom(d).GetTypes().Where(a => a.IsPublic && a.IsClass)
.Select (a => new { Parent = a.BaseType, Name = a.Name}))
.Where (d => d.Parent!=null)
.Select (a => new { Parent = a.Parent.Name , Name = a.Name})
.ToLookup (a => a.Parent )
.Take(10)
.Dump();
