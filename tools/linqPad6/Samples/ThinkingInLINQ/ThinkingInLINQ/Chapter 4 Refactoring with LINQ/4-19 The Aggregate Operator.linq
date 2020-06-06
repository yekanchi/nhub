<Query Kind="Statements">
  
</Query>

Console.WriteLine("Printing all names comma separated using loop");
string[] names = {"Greg","Travis","Dan"};
for (int k = 0; k< names.Length - 1; k++)
	Console.Write(names[k]+",");
//Printing the last name (one off logic)
Console.Write(names[names.Length - 1]);
Console.WriteLine();
Console.WriteLine("Printing all names comma separated using LINQ");
names.Aggregate ((f,s)=>f +"," + s).Dump();
