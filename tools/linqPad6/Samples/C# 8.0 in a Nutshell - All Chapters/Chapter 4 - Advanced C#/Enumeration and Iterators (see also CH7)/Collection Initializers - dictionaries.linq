<Query Kind="Statements" />

var dict1 = new Dictionary<int, string>()
{
	{ 5, "five" },
	{ 10, "ten" }
};

dict1.Dump();

var dict2 = new Dictionary<int, string>()
{
	[3] = "three",
	[10] = "ten"
};

dict2.Dump();