<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Directory.GetFiles(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5","*.dll")
.SelectMany (d => Assembly.LoadFrom(d).GetTypes()
.Where (a => a.IsClass && a.IsPublic)
.Select ( s =>
new
{
	TypeName = s.FullName,
	MethodCount = s.GetMethods()
					.Count(m => m.IsPublic
							&& !m.Name.StartsWith("get_")
							&& !m.Name.StartsWith("set_"))
})
)
.OrderByDescending (d => d.MethodCount)
.Take(10)
.Dump();
