<Query Kind="Statements">
  
</Query>

string[] input = {"ABC", "DEF", "G", "H"};
string result =
"{" //starting/opening brace
+ input.Take(input.Length - 1).Aggregate((f,s) => f + ", " + s)
+ " and "
+ input.Last()
+ "}";//closing brace
result.Dump("Eric's Comma Quibbling");
