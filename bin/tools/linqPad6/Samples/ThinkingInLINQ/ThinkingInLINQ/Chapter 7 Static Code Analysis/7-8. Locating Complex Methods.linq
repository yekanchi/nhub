<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

//Locate highly complex methods with lots of arguments
Directory.GetFiles(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5","*.dll")
.SelectMany (d => Assembly.LoadFrom(d).GetTypes()
.SelectMany (a => a.GetMethods()))
.Where (d => !d.Name.StartsWith("get_")
     && !d.Name.StartsWith("set_"))
.Select (d => new { MethodName = d.Name,NameSpace = d.DeclaringType.Namespace,Class = d.DeclaringType.FullName,NumberOfParameters = d.GetParameters().Count()} )
.Where (d => d.NameSpace=="System.Linq")
.OrderByDescending (d => d.NumberOfParameters )
.Take(20)
.Dump();
