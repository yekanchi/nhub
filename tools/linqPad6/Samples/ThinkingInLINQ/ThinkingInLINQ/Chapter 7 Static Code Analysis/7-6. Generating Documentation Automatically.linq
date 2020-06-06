<Query Kind="Program">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

public string GetSummary(string total, string methodName)
{
	string search = methodName;
	string summary = total.Substring(
	total.IndexOf(search)+search.Length);
	summary = summary.Substring(
	summary.IndexOf("<summary>")+"<summary>".Length);
	summary = summary.Substring(0,summary.IndexOf("</summary"));
	return summary;
}
void Main()
{
	string moreLINQdll = @"C:\MoreLINQ\MoreLINQ.dll";
	string xmlFilePath = @"C:\MoreLINQ\MoreLinq.xml";
	StreamReader sr = new StreamReader (xmlFilePath);
	string total = sr.ReadToEnd();
	sr.Close();
	total = total
			.Replace("<c>",string.Empty).Replace("</c>",string.Empty)
			.Replace("&lt;","<").Replace("&gt;",">");
	var allMethods = Assembly
	.LoadFrom(moreLINQdll)
	.GetTypes()
	
	.Where (a => a.IsPublic )
	.ToList()
	.Select(t => new KeyValuePair<string,
	List<KeyValuePair<string,string>>>(t.Name,t.GetMethods()
	.Where (x => x.IsPublic
					&& (!x.Name.StartsWith("get_")
					&& !x.Name.StartsWith("set_")
					&& !x.Name.StartsWith("GetHashCode")
					&& !x.Name.StartsWith("ToString")
					&& !x.Name.StartsWith("Equals")
					&& !x.Name.StartsWith("CompareTo")
					&& !x.Name.StartsWith("GetType")))
	.Select (x => new KeyValuePair<string,string>(x.Name, GetSummary(total,t.Name+"."+x.Name)))
	.DistinctBy(z => z.Key)
	.ToList()))
	.First()
	.Dump();
}
