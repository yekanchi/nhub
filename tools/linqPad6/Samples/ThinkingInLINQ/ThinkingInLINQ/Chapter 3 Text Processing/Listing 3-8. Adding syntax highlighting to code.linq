<Query Kind="Statements">
  
</Query>

/*
	To run this code you shall need Syntax Highlighter 
	Download from http://alexgorbatchev.com/SyntaxHighlighter/ 
	Download the files needed:
	

*/

Dictionary<string, List<string>> langKeywords = new Dictionary<string, List<string>>();
string directory = @"C:\syntaxhighlighter_3.0.83";
string[] files = Directory.GetFiles(directory,"*.js",SearchOption.AllDirectories);
foreach (string file in files)
{
	try
	{
			string key = new FileInfo(file).Name.Replace("shBrush", string.Empty)
			.Replace(".js", string.Empty);
			langKeywords.Add(key, new List<string>());
			langKeywords[key] = Regex.Matches(File.ReadAllText(file)
									.Split(';')
									.FirstOrDefault(m => m.Contains("var keywords"))
									.Split('=')[1], "[a-z]+")
									.Cast<Match>()
									.Select(m => m.Value).ToList();
	}
	catch
	{
		continue;
	}
}
//SampleCode.txt file is available under Chapter 3 folder.
//Place it in C:\ drive or elsewhere and then change this path accordingly.
string sampleCode = File.ReadAllText("C:\\SampleCode.txt");
Dictionary<string,int> confiMap = new Dictionary<string,int>();
foreach (var lang in langKeywords.Keys)
{
	foreach(var kw in langKeywords[lang])
	{
		if(sampleCode.Contains(kw))
		{
			if(!confiMap.ContainsKey(lang))
				confiMap.Add(lang,1);
			else
				confiMap[lang]++;
		}
	}
}

confiMap .OrderByDescending (m => m.Value ).Dump();
Dictionary<string,string> brushAliases = new Dictionary<string,string>();
brushAliases.Add("CSharp","csharp");
brushAliases.Add("Python","python");
brushAliases.Add("Ruby","ruby");
brushAliases.Add("Perl","perl");
brushAliases.Add("CPP","cpp");

var commonKeys = confiMap.Where (m => brushAliases.Select (a => a.Key).Contains( m.Key)).Select (m => m.Key);
//commonKeys.Dump();
confiMap = confiMap.Where (m => commonKeys.Contains(m.Key))
                   .ToDictionary (k => k.Key,k=>k.Value);
StreamReader sr = new StreamReader ("C:\\rudiSynTemplate.html");
string total = sr.ReadToEnd();
sr.Close();
total = total.Replace("{brushAlias}",brushAliases[confiMap.OrderByDescending (m => m.Value ).First ().Key]);
total = total.Replace("<code>",sampleCode);

StreamWriter sw = new StreamWriter (@"C:\syntaxhighlighter_3.0.83\synh.html");
sw.WriteLine(total);
sw.Close();
System.Diagnostics.Process.Start(@"C:\syntaxhighlighter_3.0.83\synh.html");
