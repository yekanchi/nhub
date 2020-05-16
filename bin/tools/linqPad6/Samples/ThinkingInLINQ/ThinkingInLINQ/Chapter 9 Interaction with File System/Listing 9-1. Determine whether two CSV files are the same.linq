<Query Kind="Statements">
  <Reference>C:\Evaluant.Linq.Compiler.dll</Reference>
  
  <Namespace>Evaluant.Linq.Compiler</Namespace>
  <Namespace>MoreLinq</Namespace>
</Query>

Func<string,IEnumerable<string>> GetHeaders = (fileName) => File.ReadAllLines(fileName)
																.First()
																.Split(new char[]{','},StringSplitOptions.None);

Func<string,IEnumerable<string>> GetBody = (fileName) => File.ReadAllLines(fileName)
															 .Skip(1)
															 .Where (f => f.Trim().Length!=0);
Func<string,string,bool> IsSameCSV = (firstFile,secondFile) =>
										//Match column headers
									    GetHeaders(firstFile)
												.All (x => GetHeaders(secondFile).Contains(x))
										
										&& //Match the body 
										GetBody(firstFile)
												.All (x => GetBody(secondFile).Contains(x));
