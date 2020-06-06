<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Directory.GetFiles(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5","*.dll")
.SelectMany (d => Assembly.LoadFrom(d).GetTypes()
.SelectMany (a => a.GetMethods()))
.Where (d => d.IsPublic
&& d.DeclaringType.Namespace=="System.Linq"
&& !d.Name.StartsWith("get_")
&& !d.Name.StartsWith("set_"))
.ToLookup (d => d.Name)
.Select (d => new { MethodName = d.Key,Overloads = d.Count ()})
//Overloads = 1 doesn't make sense.
.Where (d => d.Overloads>=2)
.OrderByDescending (d => d.Overloads)
.Take(10)//Show only the top 10 entries
.Dump();
