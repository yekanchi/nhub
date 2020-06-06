<Query Kind="Statements">
  
</Query>

string algae = "A";
Func<string,string> transformA = x => x.Replace("A","AB");
Func<string,string> markBs = x => x.Replace("B","[B]");
Func<string,string> transformB = x => x.Replace("[B]","A");
int length = 7;
Enumerable.Range(1,length)
		  .Select (k => new KeyValuePair<int,string>(k,algae = transformB(transformA(markBs(algae)) )))
		  
.Dump("Showing the growth of the algae as described by L-System");
