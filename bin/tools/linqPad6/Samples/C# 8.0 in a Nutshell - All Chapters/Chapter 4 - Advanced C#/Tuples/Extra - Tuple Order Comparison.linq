<Query Kind="Statements" />

var tuples = new[]
{
	("B", 50),
	("B", 40),
	("A", 30),
	("A", 20)
};

tuples.OrderBy (x => x).Dump ("They're all now in order!");