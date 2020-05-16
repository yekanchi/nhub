<Query Kind="Statements">
  
</Query>

//Reversing a sentence word-by-word
string line = "nothing know I";
line.Split(' ').Aggregate ((a,b) => b + " " + a).Dump();
