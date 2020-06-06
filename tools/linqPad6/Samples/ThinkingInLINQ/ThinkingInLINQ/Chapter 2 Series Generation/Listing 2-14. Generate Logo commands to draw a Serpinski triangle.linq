<Query Kind="Statements">
  
</Query>

string serpinskiTriangle = "A";
Func<string,string> transformA = x => x.Replace("A","B-A-B");
Func<string,string> markBs = x => x.Replace("B","[B]");
Func<string,string> transformB = x => x.Replace("[B]","A+B+A");
int length = 6;
Enumerable.Range(1,length)
		  .ToList()
	      .ForEach (k => serpinskiTriangle = transformB(transformA(markBs(serpinskyTriangle))));
		  

serpinskiTriangle
	.Replace("A", "forward 5" + Environment.NewLine)
	.Replace("B", "forward 5" + Environment.NewLine)
	.Replace("+", "left 60" + Environment.NewLine)
	.Replace("-", "right 60" + Environment.NewLine)
	.Dump("LOGO Commands for drawing Serpinsky Triangle");
