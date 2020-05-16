<Query Kind="Statements">
  
</Query>

string[] salutations =
{"Mr.", "Mrs.","Master.","Ms."};
string[] names = {"Patrick","Nancy","Jon","Jane"};
List<string> allNames = new List<string>();
for(int i=0; i< salutations.Length; i++)
	allNames.Add(salutations[i] + " " + names[i] + " Smith");

allNames.Dump("From Loop");


salutations.Zip(names, (salutation, name ) => salutation + " " + name + " Smith")
           .Dump("From LINQ");
