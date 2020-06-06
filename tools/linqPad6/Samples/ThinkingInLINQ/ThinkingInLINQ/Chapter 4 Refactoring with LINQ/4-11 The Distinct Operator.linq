<Query Kind="Statements">
  
</Query>

string[] names = {"Sam","David","Sam","Eric","Daniel","Sam"};
Array.Sort(names);
List<string> distinctNames = new List<string>();
for(int i=0;i<names.Length - 1 ;i++)
{
	if(names[i]!=names[i+1])
		distinctNames.Add(names[i]);
	else
	{
		if(distinctNames[distinctNames.Count -	1]!= names[i])
		distinctNames.Add(names[i]);
	}
}
distinctNames.Dump("Unique names from Loop");

names.Distinct().Dump("Unique names from LINQ");
