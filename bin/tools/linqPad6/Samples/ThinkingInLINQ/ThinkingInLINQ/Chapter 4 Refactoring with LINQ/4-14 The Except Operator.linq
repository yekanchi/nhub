<Query Kind="Statements">
  
</Query>

string[] names1 = {"Sam","David","Eric","Daniel"};
string[] names2 = {"David","Eric","Samuel"};
List<string> exclusiveNames = new List<string>();
for(int i=0;i<names1.Length;i++)
{
	if(Array.FindIndex(names2, m => m == names1[i])==-1)
		exclusiveNames.Add(names1[i]);
}
exclusiveNames.Dump("Names appearing only in \"names1\" but not in \"names2\" _ Found using Loop");

names1.Except(names2).Dump("Names appearing only in \"names1\" but not in \"names2\" _ Found using LINQ");
