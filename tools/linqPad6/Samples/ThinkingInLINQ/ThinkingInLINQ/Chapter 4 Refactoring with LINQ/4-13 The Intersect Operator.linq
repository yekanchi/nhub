<Query Kind="Statements">
  
</Query>

string[] names1 = {"Sam","David","Sam","Eric","Daniel","Sam"};
string[] names2 = {"David","Eric","Samuel"};
List<string> commonNames = new List<string>();
for(int i=0;i<names1.Length;i++)
{
	if(Array.FindIndex(names2, m => m ==names1[i])!=-1)
		commonNames.Add(names1[i]); 
}
commonNames.Dump("Common names found using Loop");

names1.Intersect(names2).Dump("Common names found using LINQ");
