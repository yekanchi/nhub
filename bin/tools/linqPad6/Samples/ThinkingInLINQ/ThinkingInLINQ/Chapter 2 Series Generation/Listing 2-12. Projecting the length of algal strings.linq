<Query Kind="Statements">
  
</Query>

int length = 5;
Enumerable.Range(1,length)
.Select (k => new Tuple<int,string,int>(k,algae =
                   transformB(transformA(markBs(algae))),algae.Length) )
.Dump("The length of the alage forms the Fibonacci Series");
